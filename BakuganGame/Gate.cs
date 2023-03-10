using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakuganGame
{
    /*
    Ворота являются исчисляемой единицей поля. Каждая карта ворот:
        * Имеет координаты на поле
        * Количество бакуганов на ней
        * Ссылается на карту ворот (конкретный тип)
    */
    internal class Gate
    {
        
        public int x { get; private set; }
        public int y { get; private set; }

        public bool isBusy = false;// стоит чья-то карта
        public bool isActivated = false;
        public uint bakuganCount { get; set; }

        public uint gateOwner { get; private set; }// 0 - ничья, иначе айди бойца

        Field field; // Воротам важно знать что происходит вокруг
        public Bakugan[] bakugan; //ссылка на бакуганов установленных на карте
        public GateCard gateCard; //установленная карта ворот

        public Gate(uint NbrBaku, uint NbrTeam, uint NbrBraw, int x, int y) 
        {
            this.x = x;
            this.y = y;

            bakuganCount = 0;

            bakugan = new Bakugan[NbrBaku * NbrBraw];
            gateCard = new GateCard();

        }

        /// <summary>
        /// Поместить карту gateID от бойца brawlerID 
        /// </summary>
        /// <param name="brawlerID">ID игрока</param>
        /// <param name="gateID">ID карты ворот у игрока</param>
        /// <returns>Возвращает true - если установить бакугана удалось</returns>
        public bool placePlayerGate(uint brawlerID, uint gateID)
        {
            isBusy = true;
            gateOwner = brawlerID;
            gateCard = field.brawler[brawlerID].gateCard[gateID];
            field.brawler[brawlerID].gateCard[gateID].isPlaced = true;

            return true;
        }


        /// <summary>
        /// Удалить с ворот уже установленную карту
        /// </summary>
        /// <returns>Возвращает true - если удалось удалить карту</returns>
        public bool removePlayerGate()
        {
            if (isBusy)
            {
                gateCard.removeFromField();
                isBusy = false;
                isActivated = false;
                bakuganCount = 0;
                gateCard = new GateCard();

                return true;
            }
            else
            {
                Console.WriteLine("Gate message: ERROR in removePlayerGate function");
                Console.WriteLine("Why do you try to erase an empty gate?");

                return false;
            }
        }


        /// <summary>
        /// Закрепить за воротами ссылку на всё поле
        /// </summary>
        /// <param name="field">ссылка на поле</param>
        /// <returns>Возвращает true - если установить бакугана удалось</returns>
        public bool setField(Field field)
        {
            this.field = field;
            gateCard.setField(field);

            return true;
        }


        public void printInfo()
        {
            Console.WriteLine($"x position: {x}");
            Console.WriteLine($"y position: {y}");
            Console.WriteLine($"is busy: {isBusy}"); 
            Console.WriteLine($"is activated: {isActivated}");
            Console.WriteLine($"count of bakugan in gate: {bakuganCount}");
        }

    }
}
