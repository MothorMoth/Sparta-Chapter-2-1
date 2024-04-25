namespace SpartaDungeon
{
    public class Item
    {
        public string Name;
        public int AttackPower;
        public int DefensePower;
        public string Description;
        public int Price;
        public bool IsPurchased;
        public bool IsEquipped;

        public Item(string name, int attackPower, int defensePower, string description, int price)
        {
            Name = name;
            AttackPower = attackPower;
            DefensePower = defensePower;
            Description = description;
            Price = price;
            IsPurchased = false;
            IsEquipped = false;
        }
    }

    public class Player
    {
        public int Level;
        public string Name;
        public string Job;
        public int AttackPower;
        public int DefensePower;
        public int HealthPoint;
        public int Gold;
        public List<Item> PurchasedItems;

        public Player(int level, string name, string job, int attackPower, int defensePower, int healthPoint, int gold)
        {
            Level = level;
            Name = name;
            Job = job;
            AttackPower = attackPower;
            DefensePower = defensePower;
            HealthPoint = healthPoint;
            Gold = gold;
            PurchasedItems = new List<Item>();
        }

        public int CalculateTotalAttackPower()
        {
            int totalAttackPower = 0;
            foreach (Item item in PurchasedItems)
            {
                if (item.IsEquipped)
                {
                    totalAttackPower += item.AttackPower;
                }
            }
            return totalAttackPower;
        }

        public int CalculateTotalDefensePower()
        {
            int totalDefensePower = 0;
            foreach (Item item in PurchasedItems)
            {
                if (item.IsEquipped)
                {
                    totalDefensePower += item.DefensePower;
                }
            }
            return totalDefensePower;
        }
    }

    internal class Program
    {
        public static Item[] items = new Item[7];
        public static Player player = new Player(1, "Chad", "전사", 10, 5, 100, 10000);

        static void InitializeItems()
        {
            items[0] = new Item("수련자 갑옷", 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000);
            items[1] = new Item("무쇠 갑옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1800);
            items[2] = new Item("스파르타의 갑옷", 0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500);
            items[3] = new Item("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검입니다.", 600);
            items[4] = new Item("청동 도끼", 5, 0, "어디선가 사용됐던거 같은 도끼입니다.", 1500);
            items[5] = new Item("스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2700);
            items[6] = new Item("스파르타 몽둥이", 100, 0, "TIL을 안 쓰면 볼 수 있습니다.", 10000);
        }

        static void ShowVillage()
        {
            bool isPlaying = true;

            while (isPlaying)
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("0. 게임 종료");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해 주세요.");
                Console.Write(">> ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowStatus();
                        break;

                    case "2":
                        ShowInventory();
                        break;

                    case "3":
                        ShowStore();
                        break;

                    case "0":
                        isPlaying = false;
                        break;

                    default:
                        PrintInputError();
                        break;
                }
            }
        }

        static void PrintInputError()
        {
            Console.WriteLine();
            Console.Write("잘못된 입력입니다.");
            Console.ReadLine();
        }

        static void ShowStatus()
        {
            while (true)
            {
                int totalItemAttackPower = player.CalculateTotalAttackPower();
                int totalItemDefensePower = player.CalculateTotalDefensePower();

                Console.Clear();
                Console.WriteLine("상태보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine();
                Console.WriteLine($"Lv. {player.Level}");
                Console.WriteLine($"{player.Name} ( {player.Job} )");
                if (totalItemAttackPower > 0)
                {
                    Console.WriteLine($"공격력 : {player.AttackPower} + ({totalItemAttackPower})");
                }
                else
                {
                    Console.WriteLine($"공격력 : {player.AttackPower}");
                }

                if (totalItemDefensePower > 0)
                {
                    Console.WriteLine($"방어력 : {player.DefensePower} + ({totalItemDefensePower})");
                }
                else
                {
                    Console.WriteLine($"방어력 : {player.DefensePower}");
                }
                Console.WriteLine($"체 력 : {player.HealthPoint}");
                Console.WriteLine($"Gold : {player.Gold} G");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해 주세요.");
                Console.Write(">> ");

                string input = Console.ReadLine();

                if (input == "0")
                {
                    break;
                }
                else
                {
                    PrintInputError();
                }
            }
        }

        static void ShowInventory()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < player.PurchasedItems.Count; i++)
                {
                    string equippedStatus = player.PurchasedItems[i].IsEquipped ? $"[E] {player.PurchasedItems[i].Name}" : $"{player.PurchasedItems[i].Name}";
                    string mainPower = player.PurchasedItems[i].AttackPower == 0 ? $"방어력 +{player.PurchasedItems[i].DefensePower}" : $"공격력 +{player.PurchasedItems[i].AttackPower}";

                    Console.WriteLine($"- {equippedStatus} | {mainPower} | {player.PurchasedItems[i].Description}");
                }
                Console.WriteLine();
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해 주세요.");
                Console.Write(">> ");

                string input = Console.ReadLine();

                if (input == "1")
                {
                    ShowEquipManagement();
                }
                else if (input == "0")
                {
                    break;
                }
                else
                {
                    PrintInputError();
                }
            }
        }

        static void ShowEquipManagement()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < player.PurchasedItems.Count; i++)
                {
                    string equippedStatus = player.PurchasedItems[i].IsEquipped ? $"[E] {player.PurchasedItems[i].Name}" : $"{player.PurchasedItems[i].Name}";
                    string mainPower = player.PurchasedItems[i].AttackPower == 0 ? $"방어력 +{player.PurchasedItems[i].DefensePower}" : $"공격력 +{player.PurchasedItems[i].AttackPower}";

                    Console.WriteLine($"- {i + 1} {equippedStatus} | {mainPower} | {player.PurchasedItems[i].Description}");
                }
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해 주세요.");
                Console.Write(">> ");

                string input = Console.ReadLine();
                int selectNum;
                bool isint = int.TryParse(input, out selectNum);

                if (isint)
                {
                    if (selectNum > 0 && selectNum <= player.PurchasedItems.Count)
                    {
                        player.PurchasedItems[selectNum - 1].IsEquipped = !(player.PurchasedItems[selectNum - 1].IsEquipped);
                    }
                    else if (selectNum == 0)
                    {
                        break;
                    }
                    else
                    {
                        PrintInputError();
                    }
                }
                else
                {
                    PrintInputError();
                }
            }
        }

        static void ShowStore()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < items.Length; i++)
                {
                    string purchaseStatus = items[i].IsPurchased ? "구매 완료" : $"{items[i].Price} G";
                    string mainPower = items[i].AttackPower == 0 ? $"방어력 +{items[i].DefensePower}" : $"공격력 +{items[i].AttackPower}";

                    Console.WriteLine($"- {items[i].Name} | {mainPower} | {items[i].Description} | {purchaseStatus}");
                }
                Console.WriteLine();
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                string input = Console.ReadLine();

                if (input == "1")
                {
                    ShowPurchaseItem();
                }
                else if (input == "2")
                {
                    ShowSellItem();
                }
                else if (input == "0")
                {
                    break;
                }
                else
                {
                    PrintInputError();
                }
            }
        }

        static void ShowPurchaseItem()
        {
            bool isPurchasing = true;

            while (isPurchasing)
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 구매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < items.Length; i++)
                {
                    string purchaseStatus = items[i].IsPurchased ? "구매 완료" : $"{items[i].Price} G";
                    string mainPower =items[i].AttackPower == 0 ? $"방어력 +{items[i].DefensePower}" : $"공격력 +{items[i].AttackPower}";

                    Console.WriteLine($"- {i + 1} {items[i].Name} | {mainPower} | {items[i].Description} | {purchaseStatus}");
                }
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                string input = Console.ReadLine();
                int selectNum;
                bool isint = int.TryParse(input, out selectNum);

                if (isint)
                {
                    if (selectNum > 0 && selectNum <= items.Length)
                    {
                        ProcessPurchase(selectNum);
                    }
                    else if (selectNum == 0)
                    {
                        isPurchasing = false;
                    }
                    else
                    {
                        PrintInputError();
                    }
                }
                else
                {
                    PrintInputError();
                }
            }
        }

        static void ProcessPurchase(int selectNum)
        {
            int i = selectNum - 1;

            if (items[i].IsPurchased == false)
            {
                if (player.Gold >= items[i].Price)
                {
                    player.Gold -= items[i].Price;
                    items[i].IsPurchased = true;
                    player.PurchasedItems.Add(items[i]);

                    Console.WriteLine();
                    Console.Write("구매를 완료했습니다.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine();
                    Console.Write("Gold 가 부족합니다.");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine();
                Console.Write("이미 구매한 아이템입니다.");
                Console.ReadLine();
            }
        }

        static void ShowSellItem()
        {
            bool isSelling = true;

            while (isSelling)
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 판매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < player.PurchasedItems.Count; i++)
                {
                    string equippedStatus = player.PurchasedItems[i].IsEquipped ? $"[E] {player.PurchasedItems[i].Name}" : $"{player.PurchasedItems[i].Name}";
                    string mainPower = player.PurchasedItems[i].AttackPower == 0 ? $"방어력 +{player.PurchasedItems[i].DefensePower}" : $"공격력 +{player.PurchasedItems[i].AttackPower}";

                    Console.WriteLine($"- {i + 1} {equippedStatus} | {mainPower} | {player.PurchasedItems[i].Description} | {player.PurchasedItems[i].Price} G");
                }
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                string input = Console.ReadLine();
                int selectNum;
                bool isint = int.TryParse(input, out selectNum);

                if (isint)
                {
                    if (selectNum > 0 && selectNum <= player.PurchasedItems.Count)
                    {
                        if (player.PurchasedItems[selectNum - 1].IsEquipped == true)
                        {
                            player.PurchasedItems[selectNum - 1].IsEquipped = false;
                        }

                        player.Gold += (int)(player.PurchasedItems[selectNum - 1].Price * 0.85f);
                        player.PurchasedItems[selectNum - 1].IsPurchased = false;
                        player.PurchasedItems.Remove(player.PurchasedItems[selectNum - 1]);
                    }
                    else if (selectNum == 0)
                    {
                        isSelling = false;
                    }
                    else
                    {
                        PrintInputError();
                    }
                }
                else
                {
                    PrintInputError();
                }
            }
        }

        static void Main()
        {
            InitializeItems();

            ShowVillage();
        }
    }
}
