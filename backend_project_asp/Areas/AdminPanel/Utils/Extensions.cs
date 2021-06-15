﻿using Microsoft.AspNetCore.Http;

namespace P316Practise.Areas.AdminPanel.Utils
{
    public static class Extensions
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image/");
        }

        public static bool IsSizeAllowed(this IFormFile file, int kb)
        {
            return file.Length < kb * 1000;
        }
    }
}
