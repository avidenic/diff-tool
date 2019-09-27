using System;
using System.Collections.Generic;
using System.Linq;
using Descartes.Demo.Infrastructure;

namespace Descartes.Demo.Models
{
    public class Comparison
    {
        private byte[][] dataToDiff = new byte[2][];

        public string this[Side index]
        {
            set
            {
                dataToDiff[(int)index] = Convert.FromBase64String(value);
                // reset cache
                this.differences.Clear();
            }
        }

        private IList<Difference> differences = new List<Difference>();

        /// <summary>
        /// Gets the differences between compared sides
        /// </summary>
        /// <returns>A collection of differences. It is empty if no differences were found or null if lengths are not equal./returns>
        /// <exception cref="Descartes.Demo.Infrastructure.BusinessRuleException">Thrown if any of the sides cannot be compared i.e. is null.</exception> 
        public IReadOnlyCollection<Difference> GetDifferences()
        {
            // return cached data
            if (this.differences.Any())
            {
                return this.differences.ToArray();
            }

            // if any of the sides is empty throw exception
            if (dataToDiff.Any(x => x == null))
            {
                throw new BusinessRuleException("At least one side is null");
            }

            // if lengths are different, we do not calculate differences
            if (dataToDiff[0].Length != dataToDiff[1].Length)
            {
                return null;
            }
            
            var currentDiffLength = 0;
            var length = dataToDiff[0].Length;
            for (int i = 0; i < length; i++)
            {
                // if data is different, increase current diff length and check next byte
                if (dataToDiff[0][i] != dataToDiff[1][i])
                {
                    currentDiffLength += 1;
                    continue;
                }

                // currently there is no difference, but was in previous comparison, so add it
                if (currentDiffLength > 0)
                {
                    differences.Add(new Difference { Offset = i - currentDiffLength, Length = currentDiffLength });
                    currentDiffLength = 0;
                }
            }

            // chek leftovers
            if (currentDiffLength > 0)
            {
                differences.Add(new Difference { Offset = length - currentDiffLength, Length = currentDiffLength });
            }

            return this.differences.ToArray();
        }
    }
}