using System;
using System.Text;

/*
The RDATA for an RRSIG RR consists of a 2 octet Type Covered field, a
   1 octet Algorithm field, a 1 octet Labels field, a 4 octet Original
   TTL field, a 4 octet Signature Expiration field, a 4 octet Signature
   Inception field, a 2 octet Key tag, the Signer's Name field, and the
   Signature field.

                        1 1 1 1 1 1 1 1 1 1 2 2 2 2 2 2 2 2 2 2 3 3
    0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
   |        Type Covered           |  Algorithm    |     Labels    |
   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
   |                         Original TTL                          |
   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
   |                      Signature Expiration                     |
   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
   |                      Signature Inception                      |
   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
   |            Key Tag            |                               /
   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+         Signer's Name         /
   /                                                               /
   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
   /                                                               /
   /                            Signature                          /
   /                                                               /
   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+

 */

namespace Heijden.DNS
{
	public class RecordRRSIG : Record
	{
        public UInt16 TypeCovered;
        public QType TypeCoveredString;
        public byte Algorithm;
        public byte Labels;
        public uint OriginalTTL;
        public uint SignatureExpiration;
        public uint SignatureInception;
        public UInt16 KeyTag;
        public string SignersName;
        public string Signature;
        //argh unix/epoc time
        DateTime SignatureExpirationDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        DateTime SignatureInceptionDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        DateTime SignatureExpirationepoc;
        DateTime SignatureInceptionepoc;
        public byte[]  SignatureBase64;
		public RecordRRSIG(RecordReader rr)
		{
          /*  TypeCovered = rr.ReadBytes(2);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(TypeCovered);

            Algorithm = rr.ReadByte();
            Labels = rr.ReadByte();
            OriginalTTL = rr.ReadBytes(4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(OriginalTTL);
            SignatureExpiration = rr.ReadBytes(4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(SignatureExpiration);
            SignatureInception = rr.ReadBytes(4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(SignatureInception);

            KeyTag = rr.ReadBytes(2);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(KeyTag);
            SignersName = rr.ReadString();
            Signature = rr.ReadString();*/
            TypeCovered = rr.ReadUInt16();
            //TypeCoveredString = TypeCovered.ToString;
            TypeCoveredString = (QType)TypeCovered;
            Algorithm = rr.ReadByte();
            Labels = rr.ReadByte();
            OriginalTTL = rr.ReadUInt32();
            SignatureExpiration = rr.ReadUInt32();
            SignatureInception = rr.ReadUInt32();
            KeyTag = rr.ReadUInt16();
            SignersName = rr.ReadDomainName();
            //Signature = rr.ReadString();
            SignatureBase64 = rr.ReadBytes(256);
            
            /* y u use unsigned 32bit unix/epoc time!*/


            SignatureExpirationepoc = SignatureExpirationDateTime.AddSeconds(SignatureExpiration);
            SignatureInceptionepoc = SignatureInceptionDateTime.AddSeconds(SignatureInception);

		}

		public override string ToString()
		{
          /*  StringBuilder TypeCoveredString = new StringBuilder();
            for (int intI = 0; intI < TypeCovered.Length; intI++)
                TypeCoveredString.AppendFormat("{0:x2}", TypeCovered[intI]);*/
           /* StringBuilder OriginalTTLString = new StringBuilder();
            for (int intI = 0; intI < OriginalTTL.Length; intI++)
            * 
            * 
                OriginalTTLString.AppendFormat("{0:x2}", OriginalTTL[intI]);*/
            
            /*string TypeCoveredString = BitConverter.ToInt16(TypeCovered, 0).ToString();        
            string OriginalTTLString = BitConverter.ToInt32(OriginalTTL,0).ToString();
            string SignatureExpirationString = BitConverter.ToInt32(SignatureExpiration, 0).ToString();
            string SignatureInceptionString = BitConverter.ToInt32(SignatureInception, 0).ToString();
            string KeyTagString = BitConverter.ToInt16(KeyTag, 0).ToString();
            */
           /* StringBuilder SignatureExpirationString = new StringBuilder();
            for (int intI = 0; intI < SignatureExpiration.Length; intI++)
                SignatureExpirationString.AppendFormat("{0:x2}", SignatureExpiration[intI]);
            StringBuilder SignatureInceptionString = new StringBuilder();
            for (int intI = 0; intI < SignatureInception.Length; intI++)
                SignatureInceptionString.AppendFormat("{0:x2}", SignatureInception[intI]);

            StringBuilder KeyTagString = new StringBuilder();
            for (int intI = 0; intI < KeyTag.Length; intI++)
                KeyTagString.AppendFormat("{0:x2}", KeyTag[intI]);
            */
            //wry!

            //string SignatureExpirationString = SignatureExpirationepoc.Year + SignatureExpirationepoc.Month + SignatureExpirationepoc.Day + SignatureExpirationepoc.TimeOfDay;
            //string SignatureInceptionString = SignatureInceptionepoc.Year + SignatureInceptionepoc.Month + SignatureInceptionepoc.Day + SignatureInceptionepoc.TimeOfDay;
           StringBuilder SignatureString = new StringBuilder();
            for (int intI = 0; intI < SignatureBase64.Length; intI++)
                SignatureString.AppendFormat("{0:x2}", SignatureBase64[intI]);
                //SignatureString.Append(System.Net.IPAddress.NetworkToHostOrder(Signature[intI]));*/
            //string SignatureStringBase64;
            //SignatureStringBase64 = System.Convert.FromBase64String(SignatureString.ToString());
            return string.Format("{0} {1} {2} {3} {4}{5}{6}{7} {8}{9}{10}{11} {12} {13} {14}",
                TypeCoveredString,
                Algorithm,
                Labels,
                OriginalTTL,
                SignatureExpirationepoc.Year,
                SignatureExpirationepoc.Month,
                SignatureExpirationepoc.Day,
                SignatureExpirationepoc.Hour,
                SignatureInceptionepoc.Year,
                SignatureInceptionepoc.Month,
                SignatureInceptionepoc.Day,
                SignatureInceptionepoc.Hour,
                KeyTag,
                SignersName,
                SignatureString
                );
            
		}

	}
}
