using System;
using System.Text;
/*
See RFC4255.
 * RR type code is 44
 * The RDATA for a SSHFP RR consists of an algorithm number, fingerprint
   type and the fingerprint of the public host key.
 * algo/fp type are 1 byte, fingerprint is SHA-1 (per RFC) which is 20 bytes 
 */

namespace Heijden.DNS
{
	public class RecordSSHFP : Record
	{
        public string SSHFPrecord;
        public int ALGORITHM;
        public int DIGESTTYPE;
        byte[] fingerprint = new byte[20];
		public RecordSSHFP(RecordReader rr)
		{         
            ALGORITHM = rr.ReadByte();
            DIGESTTYPE = rr.ReadByte();
            fingerprint = rr.ReadBytes(20);
		}

		public override string ToString()
		{
            StringBuilder sb = new StringBuilder();
            for (int intI = 0; intI < fingerprint.Length; intI++)
                sb.AppendFormat("{0:x2}", fingerprint[intI]);
            return string.Format("{0} {1} {2}",
                ALGORITHM,
                DIGESTTYPE,
                sb.ToString());
		}

	}
}
