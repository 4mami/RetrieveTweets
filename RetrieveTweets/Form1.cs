using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;

namespace RetrieveTweets
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            int c = int.Parse(textBoxCount.Text);
            int num = int.Parse(textBoxNum.Text);
            string until;
            if (textBoxOldestTweetId.Text == string.Empty)
            {
                until = textBoxOldestTweetId.Text;
            }
            else
            {
                until = $"&until_id={textBoxOldestTweetId.Text}";
            }
            string usrId;

            using (var httpClient = new HttpClient())
            {
                string url = "https://api.twitter.com/2/users/by/username/" + textBoxScrNam.Text;
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer [Replace with your bearer token!!!]");

                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    using (Stream resUsr = await response.Content.ReadAsStreamAsync())
                    {
                        var dcjs = new DataContractJsonSerializer(typeof(UserJson.Rootobject));
                        UserJson.Rootobject ujro = (UserJson.Rootobject)dcjs.ReadObject(resUsr);
                        usrId = ujro.data.id;
                    }
                }
            }

            textBoxLog.Text += $"最初の取得を開始します。スクリーンネーム：{textBoxScrNam.Text}\r\n";
            int countSum = 0;
            for (int i = 0; i < c; i++)
            {
                string filePath = $"Tweets\\{today:yyyy-MM-dd}_{num}-{i + 1}_{textBoxScrNam.Text}.json";
                string url = $"https://api.twitter.com/2/users/{usrId}/tweets?max_results=100{until}";

                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        request.Headers.TryAddWithoutValidation("Authorization", "Bearer [Replace with your bearer token!!!]");

                        HttpResponseMessage response = await httpClient.SendAsync(request);
                        using (Stream resUsr = await response.Content.ReadAsStreamAsync())
                        {
                            using (StreamReader sr = new StreamReader(resUsr))
                            {
                                string resJson = sr.ReadToEnd();
                                using (StreamWriter sw = new StreamWriter(filePath))
                                {
                                    sw.Write(resJson);
                                }

                                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                                {
                                    var dcjs = new DataContractJsonSerializer(typeof(TweetJson.Rootobject));
                                    TweetJson.Rootobject tjro = (TweetJson.Rootobject)dcjs.ReadObject(fs);
                                    int count = tjro.meta.result_count;
                                    if (count < 1)
                                    {
                                        textBoxLog.Text += $"{i + 1}回目の取得時に、取得したツイート数が0になりました。\r\n";
                                        break;
                                    }
                                    countSum += count;

                                    int count2 = tjro.data.Count();
                                    textBoxLog.Text += $"{i + 1}回目の取得時の取得ツイート数：JSON内の値={count}　配列インスタンスの要素数={count2}\r\n";

                                    until = $"&until_id={tjro.meta.oldest_id}";
                                }
                            }
                        }
                    }
                }
            }
            textBoxLog.Text += $"最後の取得が終了しました。合計取得ツイート数：{countSum}\r\n\r\n";
            textBoxNum.Text = (num + 1).ToString();
        }
    }
}
