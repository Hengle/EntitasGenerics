﻿using Entitas;
using Entitas.Generics;

public sealed class MoveSystem : IExecuteSystem
{
    private readonly Contexts _contexts;
    private readonly GenericContexts _genericContexts;

    public MoveSystem(Contexts contexts, GenericContexts genericContexts)
    {
        _contexts = contexts;
        _genericContexts = genericContexts;
    }

    public void Execute()
    {
        int moveCount = 0;

        //var size = _contexts.config.mapSize.value;
        var size = _genericContexts.Config.Get<MapSizeComponent>().value;

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 1; y < size.y; y++)
            {
                var position = new GridPosition(x, y);
                var element = _contexts.game.GetEntityWithPosition(position);
                
                if(element == null)
                    continue;

                if (!element.isMovable)
                    continue;

                var targetPosition = new GridPosition(x, y - 1);
                var targetEntity = _contexts.game.GetEntityWithPosition(targetPosition);
                if (targetEntity == null)
                {
                    element.ReplacePosition(targetPosition);
                    moveCount++;
                }
            }
        }

        if (moveCount > 0)
        {
            var e = _contexts.game.CreateEntity();
            e.isFieldMoved = true;
            e.isDestroyed = true;
        }
    }
}