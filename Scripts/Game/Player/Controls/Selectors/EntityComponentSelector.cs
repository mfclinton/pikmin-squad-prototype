using Game.Core.Components.Implementations.Entity;

namespace Game.Player.Controls.Input
{
    public class EntityComponentSelector : ComponentSelectorService<EntityComponent>
    {
        // Singleton
        private static EntityComponentSelector _instance;
        public static EntityComponentSelector Instance
        {
            get => _instance ??= FindAnyObjectByType<EntityComponentSelector>();
        }
    }
}