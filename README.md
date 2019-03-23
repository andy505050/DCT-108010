# Entity Framework 6 開發實戰 (台北)

* 課程名稱: Entity Framework 6 開發實戰
* 課程日期: 2019年03月24日 (日)
* 上課時間: 09:00-17:00
* 上課地點: 台北市大安區金華街199巷5號 (淡江大學台北校區 三樓 325 教室)
* 環境設定: [Entity Framework 6 開發實戰 實作環境說明](https://gist.github.com/doggy8088/3359bde0b0425fd89f20bca062333d6a)

## 專案設定步驟


1. 透過 Git 複製本專案 (直接下載也可以)

    ```sh
    git clone https://github.com/coolrare/DCT-108010.git
    ```

2. 使用 Visual Studio 2017 開啟 `ConsoleApp1.sln` 方案檔

3. 按下 `Ctrl-Shift-B` 建置專案，確認建置無誤即可完成。

## 建立 ContosoUniversity 範例資料庫

### 透過 Visual Studio 2017 建立資料庫

1. 滑鼠雙擊「方案總管」中的 `ContosoUniversity.sql` 檔案

2. 點擊 **Connect** 連接資料庫


    ![image](https://user-images.githubusercontent.com/88981/54868930-d7298380-4dcc-11e9-8fef-99f7412503c3.png)

3. 選取 **本機** 的 **MSSQLLocalDB** 資料庫伺服器，並按下「連接」

    ![image](https://user-images.githubusercontent.com/88981/54868956-18ba2e80-4dcd-11e9-8cca-f3e81dfc1522.png)

4. 按下「執行」按鈕，資料庫即可建立完成！

    ![image](https://user-images.githubusercontent.com/88981/54868972-5b7c0680-4dcd-11e9-888e-abc81ae142a8.png)

### 透過 Management Studio (SSMS) 建立資料庫

1. 透過 Management Studio (SSMS) 連接資料庫

    * 伺服器名稱: `(localdb)\MSSQLLocalDB`
    * 使用者驗證: **Windows 驗證**

2. 將 `ContosoUniversity.sql` 拖曳至 SSMS 視窗內並執行

3. 重新整理「物件總管」中的資料庫清單，看是否出現 `ContosoUniversity` 範例資料庫

### 透過 Management Studio (SSMS) 建立資料庫

1. 開啟命令提示字元視窗 (cmd) 並進入專案資料夾


    ```sh
    cd /d C:\Projects\DCT-108010
    ```

2. 執行以下 `osql` 命令 (若沒有顯示任何錯誤訊息，就代表建立成功)

    ```sh
    osql -E -S "(localdb)\MSSQLLocalDB" -i ContosoUniversity.sql -o nul
    ```

3. 查詢 `ContosoUniversity` 資料庫是否成功被建立

    ```sh
    osql -E -S "(localdb)\MSSQLLocalDB" -Q "SELECT name FROM sys.databases WHERE name='ContosoUniversity'"
    ``` 