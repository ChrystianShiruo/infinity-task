using System.Collections.Generic;

using UnityEngine;

using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Task.Level.Part
{
    public abstract class StaticElement : BaseLevelElement
    {
        public override List<Vector2Int> UpdateConnectorPositions()
        {
            if(connectorPositions == null)
            {
                connectorPositions = new List<Vector2Int>() { Vector2Int.RoundToInt(transform.position) };
            }
            return ConnectorPositions;
        }

        protected override bool OnTryElementInteraction()
        {
            return false;
        }

        protected override void OnDenyElementInteraction()
        {
            //TODO: negative "cannot rotate this element" feedback
            throw new System.NotImplementedException();
        }

    }
}