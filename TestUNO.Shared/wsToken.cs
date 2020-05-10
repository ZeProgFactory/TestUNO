using System;


public class wsToken
{
   public Int64 PK { get; set; } = -1;
   public DateTime dt { get; set; } = DateTime.Now;
   public string USR { get; set; } = "";

   // - - -  - - - 

   public string GetJSON()
   {
      dt = DateTime.Now;

      // return JsonConvert.SerializeObject(this);

      //return $"{{\"PK\":{PK},\"dt\":\"{dt.ToShortTimeString()}\",\"USR\":\"{ USR}\"}}";

      //return ZPF.JSON.JsonConverter.Serialize(this);

      return System.Text.Json.JsonSerializer.Serialize(this);
   }

   public string GetToken()
   {
      //return Crypto.Encrypt("StoreCheck:ZPF", GetJSON(), Crypto.DestType.Text);
      return GetJSON();
   }

   public static wsToken NewToken(long PK = -729, string Login = "MossIsTheBoss")
   {
      var t = new wsToken
      {
         PK = PK,
         USR = Login,
      };

      return t;
   }

   public static wsToken GetData(string Token)
   {
      //var json = Crypto.Decrypt("StoreCheck:ZPF", Token, Crypto.DestType.Text);
      var json = Token;

      return System.Text.Json.JsonSerializer.Deserialize<wsToken>(json);
   }

}
