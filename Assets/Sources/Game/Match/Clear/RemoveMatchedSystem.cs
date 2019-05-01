﻿using System.Collections.Generic;
using Entitas;
using Entitas.Generics;

public sealed class RemoveMatchedSystem : ReactiveSystem<GameEntity>
{
    public RemoveMatchedSystem(Contexts contexts, GenericContexts genericContexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Matched.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return !entity.isDestroyed;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.isDestroyed = true;
        }
    }
}