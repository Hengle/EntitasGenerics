//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

//using Entitas;
//using Entitas.Generics;

//public sealed class GameEventSystems : Feature {

//    public GameEventSystems(GenericContexts contexts) {
//        //Add(new ColorEventSystem(contexts)); // priority: 0
//        //Add(new GameDestroyedEventSystem(contexts)); // priority: 0
//        //Add(new PositionEventSystem(contexts)); // priority: 0
//        //Add(new SelectedRemovedEventSystem(contexts)); // priority: 0
//        //Add(new SelectedEventSystem(contexts)); // priority: 0

//        Add(EventSystemFactory.Create<GameEntity, ColorComponent>(contexts.Game));
//        Add(EventSystemFactory.Create<GameEntity, DestroyedComponent>(contexts.Game));
//        Add(EventSystemFactory.Create<GameEntity, PositionComponent>(contexts.Game));
//        Add(EventSystemFactory.Create<GameEntity, SelectedComponent>(contexts.Game, GroupEvent.Added));
//        Add(EventSystemFactory.Create<GameEntity, SelectedComponent>(contexts.Game, GroupEvent.Removed));
//    }
//}
