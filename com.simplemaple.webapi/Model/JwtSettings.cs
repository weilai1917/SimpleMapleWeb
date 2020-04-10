using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.simplemaple.webapi.Model
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
    }
}


/**
 * JSON Web Token由三部分组成，它们之间用圆点(.)连接。这三部分分别是：Header、Payload、Signature
 * 总体格式是：xxxxx.yyyyy.zzzzz
 * 
 * Header header典型的由两部分组成：token的类型（“JWT”）和算法名称（比如：HMAC SHA256或者RSA等等）。
 * {   
 *  'alg': "HS256",
 *  'typ': "JWT"
 * }
 * 
 * 
 * Payload JWT的第二部分是payload，它包含声明（要求）。声明是关于实体(通常是用户)和其他数据的声明。声明有三种类型: registered, public 和 private。
 * Registered claims : 这里有一组预定义的声明，它们不是强制的，但是推荐。比如：iss (issuer), exp (expiration time), sub (subject), aud (audience)等。
 * Public claims : 可以随意定义。
 * {
 *  "sub": '1234567890',
 *  "name": 'john',
 *  "admin":true
 * }
 */
