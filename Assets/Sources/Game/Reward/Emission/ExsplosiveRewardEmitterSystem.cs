﻿using System.Collections.Generic;
using Entitas;
using Entitas.Generics;

public sealed class ExsplosiveRewardEmitterSystem : ReactiveSystem<GameEntity>
{

    private readonly Contexts _contexts;
    private readonly GenericContexts _genericContexts;

    public ExsplosiveRewardEmitterSystem(Contexts contexts, GenericContexts genericContexts) : base(contexts.game)
    {
        _contexts = contexts;
        _genericContexts = genericContexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Matched.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isMatched && entity.isExsplosive;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        //var table = _contexts.config.ExplosiveScoringTable.value;
        var table = _genericContexts.Config.Get<ExplosiveScoringTableComponent>().value;

        var scoreId = entities.Count;
        scoreId--;

        if (scoreId >= table.Count)
            scoreId = table.Count - 1;

        var reward = table[scoreId];

        var e = _contexts.game.CreateEntity();
        e.AddReward(reward);
    }
}