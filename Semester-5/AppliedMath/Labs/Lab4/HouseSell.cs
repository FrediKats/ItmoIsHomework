using Microsoft.ML.Runtime.Api;

namespace Lab4
{
    class HouseSell
    {
        [Column("0")]
        public int Id;
        [Column("1")]
        public int MSSubClass;
        [Column("2")]
        public string MSZoning;
        [Column("3")]
        public string LotFrontage;
        [Column("4")]
        public int LotArea;
        [Column("5")]
        public string Street;
        [Column("6")]
        public string Alley;
        [Column("7")]
        public string LotShape;
        [Column("8")]
        public string LandContour;
        [Column("9")]
        public string Utilities;
        [Column("10")]
        public string LotConfig;
        [Column("11")]
        public string LandSlope;
        [Column("12")]
        public string Neighborhood;
        [Column("13")]
        public string Condition1;
        [Column("14")]
        public string Condition2;
        [Column("15")]
        public string BldgType;
        [Column("16")]
        public string HouseStyle;
        [Column("17")]
        public int OverallQual;
        [Column("18")]
        public int OverallCond;
        [Column("19")]
        public int YearBuilt;
        [Column("20")]
        public int YearRemodAdd;
        [Column("21")]
        public string RoofStyle;
        [Column("22")]
        public string RoofMatl;
        [Column("23")]
        public string Exterior1st;
        [Column("24")]
        public string Exterior2nd;
        [Column("25")]
        public string MasVnrType;
        [Column("26")]
        public int MasVnrArea;
        [Column("27")]
        public string ExterQual;
        [Column("28")]
        public string ExterCond;
        [Column("29")]
        public string Foundation;
        [Column("30")]
        public string BsmtQual;
        [Column("31")]
        public string BsmtCond;
        [Column("32")]
        public string BsmtExposure;
        [Column("33")]
        public string BsmtFinType1;
        [Column("34")]
        public int BsmtFinSF1;
        [Column("35")]
        public string BsmtFinType2;
        [Column("36")]
        public int BsmtFinSF2;
        [Column("37")]
        public int BsmtUnfSF;
        [Column("38")]
        public int TotalBsmtSF;
        [Column("39")]
        public string Heating;
        [Column("40")]
        public string HeatingQC;
        [Column("41")]
        public char CentralAir;
        [Column("42")]
        public string Electrical;
        [Column("43")]
        public int FirstFlrSF;
        [Column("44")]
        public int SecondFlrSF;
        [Column("45")]
        public int LowQualFinSF;
        [Column("46")]
        public int GrLivArea;
        [Column("47")]
        public int BsmtFullBath;
        [Column("48")]
        public int BsmtHalfBath;
        [Column("49")]
        public int FullBath;
        [Column("50")]
        public int HalfBath;
        [Column("51")]
        public int BedroomAbvGr;
        [Column("52")]
        public int KitchenAbvGr;
        [Column("53")]
        public string KitchenQual;
        [Column("54")]
        public int TotRmsAbvGrd;
        [Column("55")]
        public string Functional;
        [Column("56")]
        public int Fireplaces;
        [Column("57")]
        public string FireplaceQu;
        [Column("58")]
        public string GarageType;
        [Column("59")]
        public int GarageYrBlt;
        [Column("60")]
        public string GarageFinish;
        [Column("61")]
        public int GarageCars;
        [Column("62")]
        public int GarageArea;
        [Column("63")]
        public string GarageQual;
        [Column("64")]
        public string GarageCond;
        [Column("65")]
        public char PavedDrive;
        [Column("66")]
        public int WoodDeckSF;
        [Column("67")]
        public int OpenPorchSF;
        [Column("68")]
        public int EnclosedPorch;
        [Column("69")]
        public int ThreeSsnPorch;
        [Column("70")]
        public int ScreenPorch;
        [Column("71")]
        public int PoolArea;
        [Column("72")]
        public string PoolQC;
        [Column("73")]
        public string Fence;
        [Column("74")]
        public string MiscFeature;
        [Column("75")]
        public int MiscVal;
        [Column("76")]
        public int MoSold;
        [Column("77")]
        public int YrSold;
        [Column("78")]
        public string SaleType;
        [Column("79")]
        public string SaleCondition;
        [Column("80")]
        public int SalePrice;
    }

    public class HouseSellPricePrediction
    {
        [ColumnName("Price")]
        public int SalePrice;
    }
}
