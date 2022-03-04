using System;

namespace Dungeon
{
    public static class Events
    {
        public static event Action<int> OnChangeColorByID;
        public static void ChangeColorByID(int id) => OnChangeColorByID?.Invoke(id);
    }
}
