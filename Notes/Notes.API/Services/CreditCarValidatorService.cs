using Notes.API.Data;
using Notes.API.Models.Entities;
using System.Runtime.CompilerServices;

namespace Notes.API.Services
{
    public enum CardType
    {
        Unknown,
        Visa,
        MasterCard,
        AmEx,
        Discover
    }

    public class CreditCarValidatorService
    {
        private string ccNumber;

        public int Length { get; private set; }
        public CardType DetectedCardType { get; private set; }

        public CreditCarValidatorService(string ccNumber)
        {
            this.ccNumber = ccNumber;
            this.Length = ccNumber.Length;
            this.DetectedCardType = CardType.Unknown; // Initialize with Unknown
        }

        public bool LuhnAlgorithm()
        {
            int count = 0;

            for (int i = Length - 1; i >= 0; i--)
            {
                int currentDigit = int.Parse(ccNumber[i].ToString());

                if ((Length - i) % 2 == 0)
                {
                    currentDigit *= 2;
                    if (currentDigit > 9)
                    {
                        currentDigit = currentDigit % 10 + 1;
                    }
                }

                count += currentDigit;
            }

            return (count % 10) == 0;
        }

        public bool IsVisaCard()
        {
            return (LuhnAlgorithm() && this.ccNumber.Length == 16 && this.ccNumber.StartsWith("4"));
        }

        public bool IsMasterCard()
        {
            return (LuhnAlgorithm() && this.ccNumber.Length == 16 &&
                    (this.ccNumber.StartsWith("22") || this.ccNumber.StartsWith("51") ||
                     this.ccNumber.StartsWith("52") || this.ccNumber.StartsWith("53") ||
                     this.ccNumber.StartsWith("54") || this.ccNumber.StartsWith("55")));
        }

        public bool IsAmExCard()
        {
            return (LuhnAlgorithm() && this.ccNumber.Length == 15 &&
                    (this.ccNumber.StartsWith("37") || this.ccNumber.StartsWith("34")));
        }

        public bool IsDiscover()
        {
            return (LuhnAlgorithm() && this.ccNumber.Length == 16 && this.ccNumber.StartsWith("6011"));
        }

        public void DetermineCardType()
        {
            // Reset to Unknown before checking
            this.DetectedCardType = CardType.Unknown;

            if (IsVisaCard())
            {
                this.DetectedCardType = CardType.Visa;
            }
            else if (IsMasterCard())
            {
                this.DetectedCardType = CardType.MasterCard;
            }
            else if (IsAmExCard())
            {
                this.DetectedCardType = CardType.AmEx;
            }
            else if (IsDiscover())
            {
                this.DetectedCardType = CardType.Discover;
            }
        }
    }
}
      

