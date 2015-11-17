using System;
using UnityEngine;
using System.Collections;

public static class Statics
{

    public static EnumLevel LastLoadedLevel;

    public static string GetName(this EnumLevel level)
    {
        switch (level)
        {
            case EnumLevel.BankPresent:
                return "bankPresent";
            case EnumLevel.BankFuture:
                return "bankFuture";
            case EnumLevel.MainMenu:
                return "mainMenu";
            case EnumLevel.RiverSide:
                return "riverSide";
            case EnumLevel.SubUrbPresent:
                return "suburbPresent";
            case EnumLevel.SubUrbFuture:
                return "suburbFuture";
            default: throw new Exception();
        }

    }

    public static EnumLevel GetFromName(string name)
    {
        switch (name)
        {
            case "bankPresent":
                return EnumLevel.BankPresent;
            case "bankFuture":
                return EnumLevel.BankFuture;
            case "mainMenu":
                return EnumLevel.MainMenu;
            case "riverSide":
                return EnumLevel.RiverSide;
            case "suburbPresent":
                return EnumLevel.SubUrbPresent;
            case "suburbFuture":
                return EnumLevel.SubUrbFuture;
            default: throw new Exception();
        }

    }
}
