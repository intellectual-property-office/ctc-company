using System.ComponentModel;
using System.Runtime.Serialization;

namespace IPO.Company.Gateways.Models
{
    public enum CompanyStatus
    {
        [Description("active")]
        [EnumMember(Value = "active")]
        Active,
        [Description("dissolved")]
        [EnumMember(Value = "dissolved")]
        Dissolved,
        [Description("liquidation")]
        [EnumMember(Value = "liquidation")]
        Liquidation,
        [Description("receivership")]
        [EnumMember(Value = "receivership")]
        ReceiverShip,
        [Description("converted-closed")]
        [EnumMember(Value = "converted-closed")]
        ConvertedClosed,
        [Description("voluntary-arrangement")]
        [EnumMember(Value = "voluntary-arrangement")]
        VoluntaryArrangement,
        [Description("insolvency-proceedings")]
        [EnumMember(Value = "insolvency-proceedings")]
        InsolvencyProceedings,
        [Description("administration")]
        [EnumMember(Value = "administration")]
        Administration,
        [Description("open")]
        [EnumMember(Value = "open")]
        Open,
        [Description("closed")]
        [EnumMember(Value = "closed")]
        Closed,
        [Description("registered")]
        [EnumMember(Value = "registered")]
        Registered,
        [Description("removed")]
        [EnumMember(Value = "removed")]
        Removed
    }
}
