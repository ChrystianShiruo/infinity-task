using System.Collections.Generic;
using UnityEngine;

using Task.InGame.Managers;

namespace Task.Level.Part
{
    public abstract class PathElement : BaseLevelElement
    {
        public override List<Vector2Int> UpdateConnectorPositions()
        {
            if(connectorPositions == null)
            {
                connectorPositions = new List<Vector2Int>();
            }
            else
            {
                connectorPositions.Clear();
            }

            foreach(var item in lineRenderers)
            {
                Vector3 worldStart = item.transform.TransformPoint(item.GetPosition(0));
                Vector3 worldEnd = item.transform.TransformPoint(item.GetPosition(item.positionCount - 1));

                connectorPositions.Add(Vector2Int.RoundToInt((Vector2)worldStart));
                connectorPositions.Add(Vector2Int.RoundToInt((Vector2)worldEnd));
            }

            return ConnectorPositions;
        }

        protected override bool OnTryElementInteraction()
        {
            transform.Rotate(90f * Vector3.forward);
            return true;
        }

        protected override void OnDenyElementInteraction()
        {
            //TODO: negative "cannot rotate this element" feedback
        }

        protected override void OnTogglePower(bool on)
        {
            foreach(var item in lineRenderers)
            {
                item.widthMultiplier = on ? LevelManager.Instance.LevelVisualData.widthOn : LevelManager.Instance.LevelVisualData.widthOff;
            }
        }
    }
}