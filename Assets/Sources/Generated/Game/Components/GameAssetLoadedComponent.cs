//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly AssetLoadedComponent assetLoadedComponent = new AssetLoadedComponent();

    public bool isAssetLoaded {
        get { return HasComponent(GameComponentsLookup.AssetLoaded); }
        set {
            if (value != isAssetLoaded) {
                var index = GameComponentsLookup.AssetLoaded;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : assetLoadedComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherAssetLoaded;

    public static Entitas.IMatcher<GameEntity> AssetLoaded {
        get {
            if (_matcherAssetLoaded == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AssetLoaded);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAssetLoaded = matcher;
            }

            return _matcherAssetLoaded;
        }
    }
}
