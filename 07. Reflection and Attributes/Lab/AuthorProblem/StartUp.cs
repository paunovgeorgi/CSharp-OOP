﻿namespace AuthorProblem
{

   public class StartUp

    {

        [Author("George")]

      public static void Main()

        {

            var tracker = new Tracker();

            tracker.PrintMethodsByAuthor();

        }

    }
}