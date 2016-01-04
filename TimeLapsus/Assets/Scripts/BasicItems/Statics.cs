﻿using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;

public static class Statics
{
    public static EnumLevel LastLoadedLevel;

    private static readonly List<Tuple<EnumLevel, string>> mapping = new List<Tuple<EnumLevel, string>> {
         Tuple.New(EnumLevel.BankPresent,"bankPresent" ),
         Tuple.New(EnumLevel.BankFuture, "bankFuture"),
         Tuple.New(EnumLevel.MainMenu, "mainMenu"),
         Tuple.New(EnumLevel.RiverSide, "riverSide"),
         Tuple.New(EnumLevel.SubUrbPresent, "suburbPresent"),
         Tuple.New(EnumLevel.SubUrbPast, "suburbPast"),
         Tuple.New(EnumLevel.CityPresent, "cityPresent"),
         Tuple.New(EnumLevel.CityFuture, "cityFuture"),
         Tuple.New(EnumLevel.Village, "village"),
         Tuple.New(EnumLevel.Mayor, "mayor"),
         Tuple.New(EnumLevel.Antiquarian, "antiquarian"),
         Tuple.New(EnumLevel.Pottery, "pottery"),
         Tuple.New(EnumLevel.GameEnd, "GameEnd")
    };

    public static string GetName(this EnumLevel level)
    {
        return mapping.Where(e => e.First == level).Select(e => e.Second).First();
    }

    public static EnumLevel GetFromName(string name)
    {
        return mapping.Where(e => e.Second == name).Select(e => e.First).First();
    }

    public static Dictionary<string, int> GlobalVariables = new Dictionary<string, int>();

    public static List<InventoryItem> Inventory = new List<InventoryItem>();

    public static Dictionary<EnumItemID, InventoryItem> AllInventoryItems = new Dictionary<EnumItemID, InventoryItem>
    {
        {EnumItemID.Vase, new InventoryItem("invItemVaseName", "invItemVaseName", EnumItemID.Vase, "Vase")},
        {EnumItemID.Chip, new InventoryItem("Bábelský čip", "Čip díky kterému lze rozumět libovolnému jazyku.", EnumItemID.Chip, "Chip")}
    };

    public static Dictionary<string, EnumActorID> ActorMappings = new Dictionary<string, EnumActorID>
    {
        {"[pc]",EnumActorID.MainCharacter},
        {"[an]",EnumActorID.Lara},
        {"[bp]",EnumActorID.BankerPresent},
        {"[bf]",EnumActorID.BankerFuture},
        {"[au]",EnumActorID.VendingMachine},
        {"[rs]",EnumActorID.RobotStorage},
        {"[de]",EnumActorID.Death},
        {"[po]",EnumActorID.Potter},
        {"[ma]",EnumActorID.Mayor},
    };
}