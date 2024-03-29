using System;
using System.ComponentModel.DataAnnotations;

namespace Descartes.Demo.Infrastructure
{
    /// <summary>
    /// Checks that the value is indeed a base64 encoded string
    /// </summary>
    public class ShouldBeBase64Attribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var asString = value as string;

            if (asString == null)
            {
                return false;
            }

            Span<byte> buffer = new Span<byte>(new byte[asString.Length]);
            return Convert.TryFromBase64String(asString, buffer, out int bytesParsed);
        }
    }
}