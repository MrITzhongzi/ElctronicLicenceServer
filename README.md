# ElctronicLicenceServer 接口文档
电子证件后台

### 账号相关的接口

- 登陆 GET

    ```/Account/login```   

    示例： http://locakhost:5001/Account/login?data="用encodeComponent编码的 json 格式字符串"
    
    其中 data 要求一个  encode编码 的json对象，该对象的格式 为 
    
        ```
        {
            idNum: 370523199403311023, // 身份证
            name: "姓名", 
            phone: 17862806857
        }
        ```
---------------

- 驾驶证相关
    - 申请驾驶证接口 Get

    ```/Jsz/JszApply```    
    返回数据格式：
    ```
    {
        status="NoLic"
    }
    ```
    状态详情：
        |状态|解释|
        |-|-|
        |ok|成功|
        |Unauthorized|未登录或登录超时|
        |NoLic|用户不存在|

    - 查询驾驶证接口 Get

    ```/Jsz/GetJsz```

    返回数据格式：
    ```
    {
        status = "ok",
        info = jsz
    }
    >  public partial class Jsz
    {
        public string Name { get; set; }
        public string IdNum { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
    ```
    |状态|解释|
    |-|-|
    |Unauthorized|用户不存在|
    |ok| 成功|

    jsz字段：
    
    |属性|含义|
    |-|-|
    |name|姓名|
    |idNum| 身份证号|
    |phone|电话|
    |address|住址|
      
        


