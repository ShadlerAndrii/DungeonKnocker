using DungeonKnocker.Classes;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DungeonKnocker.Controllers
{
    class DoorController
    {
        private Stack<DoorCard> _doorDeck;
        private DoorCard _currentCard;

        public DoorCard CurrentCard => _currentCard;

        public DoorController()
        {
            InitializeDeck();
        }

        public void InitializeDeck()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Deck", "Doors.json");
            if (!File.Exists(path))
            {
                _doorDeck = new Stack<DoorCard>();
                throw new FileNotFoundException("The Doors.json file was not found at the specified path: " + path + "\nGenerated clean deck!");
            }

            string jsonString = File.ReadAllText(path);
            List<DoorCard> loadedCards = JsonSerializer.Deserialize<List<DoorCard>>(jsonString,
                new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() }
            });

            Shuffle(loadedCards);

            _doorDeck = new Stack<DoorCard>(loadedCards);
        }

        public void Shuffle(List<DoorCard> deck)
        {
            Random random = new Random();
            int n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                DoorCard value = deck[k];
                deck[k] = deck[n];
                deck[n] = value;
            }
        }

        public DoorCard DrawCard()
        {
            if (_doorDeck != null && _doorDeck.Count > 0)
            {
                _currentCard = _doorDeck.Pop();
                return _currentCard;
            }
            return null;
        }
    }
}