using System;
using System.Collections.Generic;
using System.Linq;
using Descartes.Demo.Infrastructure;

namespace Descartes.Demo.Models
{
    public class Diff
    {
        private byte[][] dataToCompare = new byte[2][];

        public string this[Side index]
        {
            set
            {
                dataToCompare[(int)index] = Convert.FromBase64String(value);
                this.differences = null;
            }
        }

        private IList<Difference> differences;

        public IReadOnlyCollection<Difference> CalculateDifferences()
        {
            if (this.differences != null && this.differences.Any())
            {
                return this.differences.ToArray();
            }

            if (dataToCompare.Any(x => x == null))
            {
                throw new BusinessRuleException("At least one side is null");
            }

            if (dataToCompare[0].Length != dataToCompare[1].Length)
            {
                return null;
            }

            this.differences = new List<Difference>();
            var currentLength = 0;
            var length = dataToCompare[0].Length;
            for (int i = 0; i < length; i++)
            {
                if (dataToCompare[0][i] != dataToCompare[1][i])
                {
                    currentLength += 1;
                    continue;
                }

                if (currentLength > 0)
                {
                    differences.Add(new Difference { Offset = i - currentLength, Length = currentLength });
                    currentLength = 0;
                }
            }
            
            // chek leftovers
            if (currentLength > 0)
            {
                differences.Add(new Difference { Offset = length - currentLength, Length = currentLength });
            }

            return this.differences.ToArray();
        }
    }
}