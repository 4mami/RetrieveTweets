# ツイート取得ツール
- Twitter APIを使用して、任意のユーザのツイートを100件ずつ取得します
- 取得したツイートは、以下のフォーマットのjsonファイルとして、_Tweets_ フォルダの下に保存されます
```
{"data":[{"id":"ツイートaID","text":"ツイートa内容"},{"id":"ツイートbID","text":"ツイートb内容"}],"meta":{"oldest_id":"取得した最初のツイートのID","newest_id":"取得した最後のツイートのID","result_count":取得したツイート数}}
```
## 開発環境
- Windows 10 Home 21H1
- Microsoft Visual Studio Community 2019 Version 16.7.6
## 注意事項
- 完全に自分用なので、エラーハンドリングはしていません
- もしこのツールを使用する場合は、_Form1.cs_ 内の`[Replace with your bearer token!!!]`と書かれている部分を自分のTwitter APIのbearerトークンで置き換えてビルドしてください
- **このツールを使用することによって生じたいかなる損害についても、作者は責任を負いません**
