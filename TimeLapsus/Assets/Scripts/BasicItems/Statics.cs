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
            case EnumLevel.MainMenu:
                return "mainMenu";
            case EnumLevel.RiverSide:
                return "riverSide";
            case EnumLevel.SubUrbs:
                return "suburb";
            default: throw new Exception();
        }

    }

    public static EnumLevel GetFromName(string name)
    {
        switch (name)
        {
            case "bankPresent":
                return EnumLevel.BankPresent;
            case "mainMenu":
                return EnumLevel.MainMenu;
            case "riverSide":
                return EnumLevel.RiverSide;
            case "suburb":
                return EnumLevel.SubUrbs;
            default: throw new Exception();
        }

    }
}
