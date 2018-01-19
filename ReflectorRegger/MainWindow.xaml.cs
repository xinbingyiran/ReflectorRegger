using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Xsl;

namespace ReflectorRegger
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {



        public int UserCount
        {
            get { return (int)GetValue(UserCountProperty); }
            set { SetValue(UserCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UserCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserCountProperty =
            DependencyProperty.Register("UserCount", typeof(int), typeof(MainWindow), new PropertyMetadata(1));

        public class ConstString
        {
            public const string StandardEdition = "Standard";
            public const string VSEdition = "VS";
            public const string VSProEdition = "VSPro";
        }

        public String[] Editions
        {
            get { return (String[])GetValue(EditionsProperty); }
            set { SetValue(EditionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Editions.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditionsProperty =
            DependencyProperty.Register("Editions", typeof(String[]), typeof(MainWindow), new PropertyMetadata(new[] { ConstString.StandardEdition, ConstString.VSEdition, ConstString.VSProEdition }));



        public String CurrentEdition
        {
            get { return (String)GetValue(CurrentEditionProperty); }
            set { SetValue(CurrentEditionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentEdition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentEditionProperty =
            DependencyProperty.Register("CurrentEdition", typeof(String), typeof(MainWindow), new PropertyMetadata(ConstString.StandardEdition));




        public String GeneratedKey
        {
            get { return (String)GetValue(GeneratedKeyProperty); }
            set { SetValue(GeneratedKeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GeneratedKey.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GeneratedKeyProperty =
            DependencyProperty.Register("GeneratedKey", typeof(String), typeof(MainWindow), new PropertyMetadata(String.Empty));



        public String RegisterKey
        {
            get { return (String)GetValue(RegisterKeyProperty); }
            set { SetValue(RegisterKeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RegisterKey.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegisterKeyProperty =
            DependencyProperty.Register("RegisterKey", typeof(String), typeof(MainWindow), new PropertyMetadata(String.Empty));



        public String ProductKey
        {
            get { return (String)GetValue(ProductKeyProperty); }
            set { SetValue(ProductKeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProductKey.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductKeyProperty =
            DependencyProperty.Register("ProductKey", typeof(String), typeof(MainWindow), new PropertyMetadata(String.Empty));


        public static RoutedCommand GenerateCommand = new RoutedCommand("Generate", typeof(MainWindow));

        public MainWindow()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(GenerateCommand, OnGenerate));
            DependencyPropertyDescriptor.FromProperty(RegisterKeyProperty, this.GetType()).AddValueChanged(this,CreateProductKey);
            DependencyPropertyDescriptor.FromProperty(UserCountProperty, this.GetType()).AddValueChanged(this, CreateProductKey);
            DependencyPropertyDescriptor.FromProperty(CurrentEditionProperty, this.GetType()).AddValueChanged(this, CreateProductKey);
        }

        private void CreateProductKey(object sender, EventArgs e)
        {
            this.ProductKey = this.CalcProductKey();
        }

        internal class Class1
        {
            // Token: 0x0600000D RID: 13 RVA: 0x00002A7C File Offset: 0x00000C7C
            public static uint smethod_0(string string_0)
            {
                long num = 0L;
                for (int i = 0; i < string_0.Length; i++)
                {
                    int num2 = (int)string_0[i];
                    for (int j = 7; j >= 0; j--)
                    {
                        bool flag = (num & 32768L) == 32768L ^ (num2 & 1 << j) != 0;
                        num = (num & 32767L) << 1;
                        if (flag)
                        {
                            num ^= 4129L;
                        }
                    }
                }
                return (uint)num;
            }

            // Token: 0x0600000E RID: 14 RVA: 0x00002B00 File Offset: 0x00000D00
            public static string smethod_1(int int_0, int int_1, int int_2)
            {
                string text = string.Format("{0:X4}-{1:X4}-{2:X4}", int_0, int_1, int_2);
                return string.Format("{0}-{1:X4}", text, Class1.smethod_0(text));
            }

            // Token: 0x0600000F RID: 15 RVA: 0x00002B40 File Offset: 0x00000D40
            public static string smethod_2(string string_0, string string_1, string string_2)
            {
                string text = string.Format("{0:X4}-{1:X4}-{2:X4}", Class1.smethod_0(string_0), Class1.smethod_0(string_1), Class1.smethod_0(string_2));
                return string.Format("{0}-{1:X4}", text, Class1.smethod_0(text));
            }

            // Token: 0x06000010 RID: 16 RVA: 0x00002B90 File Offset: 0x00000D90
            public static bool smethod_3(string string_0, string string_1)
            {
                return Class1.smethod_4(string_0) && Class1.smethod_4(string_1) && (!(string_0.Substring(0, 4) != string_1.Substring(0, 4)) || !(string_0.Substring(5, 4) != string_1.Substring(5, 4)) || !(string_0.Substring(10, 4) != string_1.Substring(10, 4)));
            }

            // Token: 0x06000011 RID: 17 RVA: 0x00002BFC File Offset: 0x00000DFC
            public static bool smethod_4(string string_0)
            {
                if (string.IsNullOrEmpty(string_0))
                {
                    return false;
                }
                string_0 = string_0.ToUpperInvariant().Trim();
                if (!Class1.regex_0.IsMatch(string_0))
                {
                    return false;
                }
                string string_ = string_0.Substring(0, 14);
                string text = string.Format("{0:X4}", Class1.smethod_0(string_));
                return string_0.EndsWith(text);
            }

            // Token: 0x0400000A RID: 10
            private static Regex regex_0 = new Regex("^[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{4}$", RegexOptions.Compiled);
        }

        internal class Class2
        {
            // Token: 0x06000014 RID: 20 RVA: 0x00002139 File Offset: 0x00000339
            internal Class2(string string_1)
            {
                this.string_0 = string_1;
            }

            // Token: 0x06000015 RID: 21 RVA: 0x00002148 File Offset: 0x00000348
            private bool method_0(char char_1, char char_2)
            {
                return char_1 == this.string_0[2] && char_2 == this.string_0[3];
            }

            // Token: 0x06000016 RID: 22 RVA: 0x0000216A File Offset: 0x0000036A
            private static void smethod_0(StringBuilder stringBuilder_0)
            {
                stringBuilder_0[2] = '0';
                stringBuilder_0[3] = '0';
            }

            // Token: 0x06000017 RID: 23 RVA: 0x00002C60 File Offset: 0x00000E60
            public static void smethod_1(string string_1, out char char_1, out char char_2)
            {
                uint num = Class1.smethod_0(string_1);
                char_1 = Class2.smethod_2((byte)((ulong)num % (ulong)((long)Class2.char_0.Length)));
                char_2 = Class2.smethod_2((byte)((ulong)num / (ulong)((long)Class2.char_0.Length) % (ulong)((long)Class2.char_0.Length)));
            }

            // Token: 0x06000018 RID: 24 RVA: 0x0000217E File Offset: 0x0000037E
            public static char smethod_2(byte byte_0)
            {
                return Class2.char_0[(int)byte_0 % Class2.char_0.Length];
            }

            // Token: 0x06000019 RID: 25 RVA: 0x0000218F File Offset: 0x0000038F
            public override string ToString()
            {
                return this.string_0;
            }

            // Token: 0x17000001 RID: 1
            // (get) Token: 0x0600001A RID: 26 RVA: 0x00002CA4 File Offset: 0x00000EA4
            public bool Boolean_0
            {
                get
                {
                    if (string.IsNullOrEmpty(this.string_0))
                    {
                        return false;
                    }
                    string text = string.Format("[{0}]", Class2.char_0);
                    return new Regex(string.Concat(
                "^A3",
                text,
                "{2}(-(",
                text,
                "{4})){5}$")).IsMatch(this.string_0);
                }
            }

            // Token: 0x17000002 RID: 2
            // (get) Token: 0x0600001B RID: 27 RVA: 0x00002197 File Offset: 0x00000397
            public int Int32_0
            {
                get
                {
                    return 3;
                }
            }

            // Token: 0x17000003 RID: 3
            // (get) Token: 0x0600001C RID: 28 RVA: 0x00002D14 File Offset: 0x00000F14
            public bool Boolean_1
            {
                get
                {
                    StringBuilder stringBuilder = new StringBuilder(this.string_0);
                    Class2.smethod_0(stringBuilder);
                    char char_;
                    char char_2;
                    Class2.smethod_1(stringBuilder.ToString(), out char_, out char_2);
                    return this.method_0(char_, char_2);
                }
            }

            // Token: 0x0400000B RID: 11
            private static readonly string char_0 = "2346789ABCDEFGHJKMNPRTWXYZ";

            // Token: 0x0400000C RID: 12
            private readonly string string_0;
        }

        internal class Class3
        {
            // Token: 0x0600001E RID: 30 RVA: 0x00002D4C File Offset: 0x00000F4C
            public static string smethod_0(int index)
            {
                switch (index)
                {
                    case 0:
                        return Class3.smethod_1();
                    case 1:
                        return Class3.smethod_2();
                    case 2:
                        return Class3.smethod_3();
                    default:
                        return "Hey there!";
                }
            }

            // Token: 0x0600001F RID: 31 RVA: 0x00002D88 File Offset: 0x00000F88
            public static string smethod_1()
            {
                Random random = new Random();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("LZ-{0}-{1}-", Class3.string_0[random.Next(0, Class3.string_0.Length)], Class3.string_0[random.Next(0, Class3.string_0.Length)]);
                stringBuilder.AppendFormat("{0}", random.Next(10000, 100000));
                stringBuilder.AppendFormat("-{0:X4}", Class1.smethod_0(stringBuilder.ToString()));
                return stringBuilder.ToString();
            }

            // Token: 0x06000020 RID: 32 RVA: 0x00002E30 File Offset: 0x00001030
            public static string smethod_2()
            {
                Random random = new Random();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("{0}-{1}-", random.Next(100, 1000), random.Next(100, 1000));
                stringBuilder.AppendFormat("{0}", random.Next(100000, 1000000));
                stringBuilder.AppendFormat("-{0:X4}", Class1.smethod_0(stringBuilder.ToString()));
                return stringBuilder.ToString();
            }

            // Token: 0x06000021 RID: 33 RVA: 0x00002EBC File Offset: 0x000010BC
            public static string smethod_3()
            {
                StringBuilder stringBuilder = new StringBuilder("A300");
                byte[] array = new byte[20];
                Random random = new Random();
                random.NextBytes(array);
                for (int i = 0; i < 5; i++)
                {
                    stringBuilder.AppendFormat("-{0}{1}{2}{3}", new object[]
                    {
                Class2.smethod_2(array[i * 4]),
                Class2.smethod_2(array[i * 4 + 1]),
                Class2.smethod_2(array[i * 4 + 2]),
                Class2.smethod_2(array[i * 4 + 3])
                    });
                }
                char c;
                char c2;
                Class2.smethod_1(stringBuilder.ToString(), out c, out c2);
                stringBuilder[2] = c;
                stringBuilder[3] = c2;
                return stringBuilder.ToString();
            }

            // Token: 0x0400000D RID: 13
            private static string string_0 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        }

        internal class Class4
        {
            // Token: 0x06000024 RID: 36 RVA: 0x00002F84 File Offset: 0x00001184
            static Class4()
            {
                XmlDocument xmlDocument = new XmlDocument
                {
                    PreserveWhitespace = true
                };
                xmlDocument.LoadXml(@"<?xml version='1.0' encoding='utf-8'?>
<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform' xmlns:msxsl='urn:schemas-microsoft-com:xslt' exclude-result-prefixes='msxsl'>
<xsl:output method='xml' indent='no' omit-xml-declaration='yes'/>
  <xsl:param name='edition'/>
  <xsl:param name='version'/>
  <xsl:param name='userspurchased'/>
  
  <xsl:template match='activationrequest'>
    <data>
      <xsl:text>
</xsl:text>
      <xsl:apply-templates/>
      <edition><xsl:value-of select='$edition'/></edition>
      <xsl:text>
</xsl:text>
      <version><xsl:value-of select='$version'/></version>
      <xsl:text>
</xsl:text>
      <userspurchased><xsl:value-of select='$userspurchased'/></userspurchased>
      <xsl:text>
</xsl:text>
    </data>
  </xsl:template>

  <xsl:template match='machinehash'>
    <machinehash>
      <xsl:value-of select='text()'/>
    </machinehash>
    <xsl:text>
</xsl:text>
  </xsl:template>

  <xsl:template match='productcode|majorversion|minorversion|serialnumber|session|edition|productname'>
    <xsl:copy>
      <xsl:value-of select='text()'/>
    </xsl:copy>
    <xsl:text>
</xsl:text>
  </xsl:template>

  <xsl:template match='productcodes'>
    <xsl:copy>
      <xsl:text>
</xsl:text>
      <xsl:apply-templates />
    </xsl:copy>
    <xsl:text>
</xsl:text>    
  </xsl:template>

  <xsl:template match='product'>
    <xsl:copy>
      <xsl:text>
</xsl:text>
      <xsl:apply-templates />
    </xsl:copy>
    <xsl:text>
</xsl:text>
  </xsl:template>

  <xsl:template match='text()'/>
</xsl:stylesheet>");
                Class4.xslCompiledTransform_0 = new XslCompiledTransform();
                Class4.xslCompiledTransform_0.Load(xmlDocument);
            }

            // Token: 0x06000025 RID: 37 RVA: 0x00002FC0 File Offset: 0x000011C0
            private static string smethod_0(string string_2)
            {
                RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(new CspParameters
                {
                    Flags = CspProviderFlags.UseMachineKeyStore
                });
                rsacryptoServiceProvider.FromXmlString(@"<RSAKeyValue>
<Modulus>zLizNmLUd4VlIWee1GXgn/KxEwcghPASQ+NUzZhbY2fTGzpW64T6yEOdHlIbhX1DX6yAz2gMZKfnpQL2aFqxh5ACFV9dONSTzuQzkqeXwFEARsMxGP3eTQSWMpwVhEcraSn1zOqMb3CRDeQpgasq0lv4HRFhbwalOifKarjEL/8=</Modulus>
<Exponent>AQAB</Exponent>
<P>8+4qCwbxpWFuJje/UURm06Uec+Mk6a7Ye9FGvzVlnh7dYK38GR+bnf8NsoMRW8IlJnipfvqqEs1qnhRAJ4j96Q==</P>
<Q>1tnh1UK2FxeVSbTXrrvKlSKAWqkjaQwLB+OQeMM5Ii3ogH++91bmO0Ku8GA4qE+r/KfypT4nECQID7i5ykkFpw==</Q>
<DP>XTEqYtgeTf6xJGy77QJi/ozg24l2OskP8A3+J2LxFb3Y+ey+maKXw38D7qVgZlv/8Xi72MVPYKuWBhraf8A4sQ==</DP>
<DQ>xBAk9FZikQQmahKr2HrqzdmkRBehhtVEo7hZOLr+wmAeklUBUfltNJsPxbApQ/8gtfoVhhIH18Tpzl8GvMCSdQ==</DQ>
<InverseQ>889tPhprihee8OsPUN7Zyf1nH3tNK4uFiGmBCR1l/JMjbK62+wcQxssD7in8dZFzf/hUXZQl++DtiBUtc4O5Tw==</InverseQ>
<D>ZNm0R12GZ17KhBtEzkNl1cW737DKH1MY3GK4GxQsKRszjx09Roba+B7+3rn6HtenghE733DVchyY69w6wQu0mj6fqZ/1ZqvmP0YH1d8otVjG2E6XhshYCJhZ7Ci0Z4n6UZwAG3NBDCtXAqNSUQY7NjPnTfcG5EkQ/nqlFJKdKLE=</D>
</RSAKeyValue>");
                byte[] bytes = Encoding.UTF8.GetBytes(string_2);
                string text = Convert.ToBase64String(rsacryptoServiceProvider.SignData(bytes, new SHA1CryptoServiceProvider()));
                return string.Concat(
                    "<activationresponse>\r\n",
                    string_2,
                    "\r\n<signature>\r\n",
                    text,
                    "\r\n</signature>\r\n</activationresponse>\r\n"
                    );
            }

            // Token: 0x06000026 RID: 38 RVA: 0x00003040 File Offset: 0x00001240
            public static string smethod_1(string string_2, string string_3, int int_0)
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(string_2);
                XmlWriterSettings xmlWriterSettings = Class4.xslCompiledTransform_0.OutputSettings.Clone();
                xmlWriterSettings.NewLineChars = "\r\n";
                xmlWriterSettings.NewLineHandling = NewLineHandling.Replace;
                StringBuilder stringBuilder = new StringBuilder();
                XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSettings);
                XsltArgumentList xsltArgumentList = new XsltArgumentList();
                xsltArgumentList.AddParam("version", "", 3);
                xsltArgumentList.AddParam("edition", "", string_3);
                xsltArgumentList.AddParam("userspurchased", "", int_0);
                Class4.xslCompiledTransform_0.Transform(xmlDocument, xsltArgumentList, xmlWriter);
                string string_4 = stringBuilder.ToString();
                return Class4.smethod_0(string_4);
            }

            // Token: 0x0400000E RID: 14
            private const string string_0 = "\r\n<RSAKeyValue>\r\n<Modulus>zLizNmLUd4VlIWee1GXgn/KxEwcghPASQ+NUzZhbY2fTGzpW64T6yEOdHlIbhX1DX6yAz2gMZKfnpQL2aFqxh5ACFV9dONSTzuQzkqeXwFEARsMxGP3eTQSWMpwVhEcraSn1zOqMb3CRDeQpgasq0lv4HRFhbwalOifKarjEL/8=</Modulus>\r\n<Exponent>AQAB</Exponent>\r\n<P>8+4qCwbxpWFuJje/UURm06Uec+Mk6a7Ye9FGvzVlnh7dYK38GR+bnf8NsoMRW8IlJnipfvqqEs1qnhRAJ4j96Q==</P>\r\n<Q>1tnh1UK2FxeVSbTXrrvKlSKAWqkjaQwLB+OQeMM5Ii3ogH++91bmO0Ku8GA4qE+r/KfypT4nECQID7i5ykkFpw==</Q>\r\n<DP>XTEqYtgeTf6xJGy77QJi/ozg24l2OskP8A3+J2LxFb3Y+ey+maKXw38D7qVgZlv/8Xi72MVPYKuWBhraf8A4sQ==</DP>\r\n<DQ>xBAk9FZikQQmahKr2HrqzdmkRBehhtVEo7hZOLr+wmAeklUBUfltNJsPxbApQ/8gtfoVhhIH18Tpzl8GvMCSdQ==</DQ>\r\n<InverseQ>889tPhprihee8OsPUN7Zyf1nH3tNK4uFiGmBCR1l/JMjbK62+wcQxssD7in8dZFzf/hUXZQl++DtiBUtc4O5Tw==</InverseQ>\r\n<D>ZNm0R12GZ17KhBtEzkNl1cW737DKH1MY3GK4GxQsKRszjx09Roba+B7+3rn6HtenghE733DVchyY69w6wQu0mj6fqZ/1ZqvmP0YH1d8otVjG2E6XhshYCJhZ7Ci0Z4n6UZwAG3NBDCtXAqNSUQY7NjPnTfcG5EkQ/nqlFJKdKLE=</D>\r\n</RSAKeyValue>\r\n";

            // Token: 0x0400000F RID: 15
            private const string string_1 = "<?xml version='1.0' encoding='utf-8'?>\r<xsl:stylesheet version='1.0' xmlns:xsl='http://www.w3.org/1999/XSL/Transform' xmlns:msxsl='urn:schemas-microsoft-com:xslt' exclude-result-prefixes='msxsl'>\r  <xsl:output method='xml' indent='no' omit-xml-declaration='yes'/>\r  <xsl:param name='edition'/>\r  <xsl:param name='version'/>\r  <xsl:param name='userspurchased'/>\r  \r  <xsl:template match='activationrequest'>\r    <data>\r      <xsl:text>\r</xsl:text>\r      <xsl:apply-templates/>\r      <edition><xsl:value-of select='$edition'/></edition>\r      <xsl:text>\r</xsl:text>\r      <version><xsl:value-of select='$version'/></version>\r      <xsl:text>\r</xsl:text>\r      <userspurchased><xsl:value-of select='$userspurchased'/></userspurchased>\r      <xsl:text>\r</xsl:text>\r    </data>\r  </xsl:template>\r\r  <xsl:template match='machinehash'>\r    <machinehash>\r      <xsl:value-of select='text()'/>\r    </machinehash>\r    <xsl:text>\r</xsl:text>\r  </xsl:template>\r\r  <xsl:template match='productcode|majorversion|minorversion|serialnumber|session|edition|productname'>\r    <xsl:copy>\r      <xsl:value-of select='text()'/>\r    </xsl:copy>\r    <xsl:text>\r</xsl:text>\r  </xsl:template>\r\r  <xsl:template match='productcodes'>\r    <xsl:copy>\r      <xsl:text>\r</xsl:text>\r      <xsl:apply-templates />\r    </xsl:copy>\r    <xsl:text>\r</xsl:text>    \r  </xsl:template>\r\r  <xsl:template match='product'>\r    <xsl:copy>\r      <xsl:text>\r</xsl:text>\r      <xsl:apply-templates />\r    </xsl:copy>\r    <xsl:text>\r</xsl:text>\r  </xsl:template>\r\r  <xsl:template match='text()'/>\r</xsl:stylesheet>\r\n";

            // Token: 0x04000010 RID: 16
            private static XslCompiledTransform xslCompiledTransform_0;
        }

        private void OnGenerate(object sender, ExecutedRoutedEventArgs e)
        {
            var index = Array.IndexOf(this.Editions, CurrentEdition);
            GeneratedKey = Class3.smethod_0(index);
        }

        private string CalcProductKey()
        {
            string text = this.RegisterKey;
            string text2 = CurrentEdition;
            int num = UserCount;
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && num != 0)
            {
                try
                {
                    return Class4.smethod_1(text, text2, num);
                }
                catch
                {
                }
            }
            return string.Empty;
        }
    }
}
