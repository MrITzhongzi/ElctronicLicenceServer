# ElctronicLicenceServer
电子证件后台

### 账号相关的接口

- 登陆
    /Account/login
    示例： http://locakhost:5001//Account/login?data="用encodeComponent编码的 json 格式字符串"
    
    其中 data 要求一个  encode编码 的json对象，该对象的格式 为 
        {
            idNum: 370523199403311023, // 身份证
            name: "姓名", 
            phone: 17862806857
        }
