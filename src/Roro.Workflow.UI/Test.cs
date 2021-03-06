﻿using System;
using System.Linq;

namespace Roro.Workflow.UI
{
    public class Test
    {

        public static void TestElementActivities()
        {
            Console.Clear();

            var desktop = new ElementBot();
            desktop.LaunchChrome("https://example.com");
            desktop.LaunchEdge("https://example.com");
            while (true)
            {
                var query = desktop.Inspect();
                var els = desktop.Query(query);
                Console.WriteLine("{0} match found..", els.Count());
                Console.WriteLine();
                foreach (var el in els)
                {
                    Console.WriteLine("{0} - {1}", el.Path, el.Bounds);
                }
            }
        }

        public static Document TestWorkflowSerialization()
        {
            var doc = new Document();
            var page = doc.Pages.First();
            page.AddNode<ProcessNode>();
            page.AddNode<DecisionNode>();
            page.AddNode<ProcessNode>();
            page.AddNode<EndNode>();

            Console.Clear();
            Console.WriteLine("Serializing Object to XML . . .");
            var a = DataContractSerializerHelper.ToString(doc);
            Console.WriteLine();
            Console.WriteLine(a);
            Console.WriteLine();
            Console.WriteLine("Serializing Object to XML completed.");
            Console.WriteLine("Deserializing XML to Object . . .");
            var o = DataContractSerializerHelper.ToObject<Document>(a);
            var b = DataContractSerializerHelper.ToString(o);
            Console.WriteLine();
            Console.WriteLine(b);
            Console.WriteLine();
            Console.WriteLine("Deserializing XML to Object completed.");
            Console.WriteLine("Matched: {0}", a == b);
            Console.WriteLine();

            return doc;

    //        var activities = AppDomain.CurrentDomain.GetAssemblies()
    //.SelectMany(x => x.GetTypes())
    //.Where(x => x.BaseType == typeof(Roro.Activities.Activity));
    //        foreach (var act in activities)
    //        {
    //            Console.WriteLine(act);
    //        }

        }
    }
}
