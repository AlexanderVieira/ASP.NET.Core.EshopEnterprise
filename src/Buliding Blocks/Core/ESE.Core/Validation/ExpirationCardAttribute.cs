﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ESE.Core.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class ExpirationCardAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var mes = value.ToString().Split('/')[0];
            var ano = $"20{value.ToString().Split('/')[1]}";

            if (int.TryParse(mes, out var month) &&
                int.TryParse(ano, out var year))
            {
                var d = new DateTime(year, month, 1);
                return d > DateTime.UtcNow;
            }

            return false;
        }
    }
}
