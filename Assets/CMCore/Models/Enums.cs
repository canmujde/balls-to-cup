namespace CMCore.Models
{
    public static class Enums
    {
        public enum Haptic
        {
            H1,
            H2,
            H3,
            H4,
            H5,
            H6,
            Success,
            Failure,
            Warning
    
        }
        public enum CurrencyType
        {
            Money,
            Diamond
        }
        public enum GameState
        {
            Menu,
            InGame,
            Fail,
            Win
        }

        public enum GameResult
        {
            Win,
            Fail
        }
    }
}
