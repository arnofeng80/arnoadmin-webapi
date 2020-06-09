using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace ArnoAdminCore.Base.Constants
{
    //public class HttpStatus
    //{
    //    public const int SUCCESS = 200;

    //    /**
    //     * 对象创建成功
    //     */
    //    public const int CREATED = 201;

    //    /**
    //     * 请求已经被接受
    //     */
    //    public const int ACCEPTED = 202;

    //    /**
    //     * 操作已经执行成功，但是没有返回数据
    //     */
    //    public const int NO_CONTENT = 204;

    //    /**
    //     * 资源已被移除
    //     */
    //    public const int MOVED_PERM = 301;

    //    /**
    //     * 重定向
    //     */
    //    public const int SEE_OTHER = 303;

    //    /**
    //     * 资源没有被修改
    //     */
    //    public const int NOT_MODIFIED = 304;

    //    /**
    //     * 参数列表错误（缺少，格式不匹配）
    //     */
    //    public const int BAD_REQUEST = 400;

    //    /**
    //     * 未授权
    //     */
    //    public const int UNAUTHORIZED = 401;

    //    /**
    //     * 访问受限，授权过期
    //     */
    //    public const int FORBIDDEN = 403;

    //    /**
    //     * 资源，服务未找到
    //     */
    //    public const int NOT_FOUND = 404;

    //    /**
    //     * 不允许的http方法
    //     */
    //    public const int BAD_METHOD = 405;

    //    /**
    //     * 资源冲突，或者资源被锁
    //     */
    //    public const int CONFLICT = 409;

    //    /**
    //     * 不支持的数据，媒体类型
    //     */
    //    public const int UNSUPPORTED_TYPE = 415;

    //    /**
    //     * 系统内部错误
    //     */
    //    public const int ERROR = 500;

    //    /**
    //     * 接口未实现
    //     */
    //    public const int NOT_IMPLEMENTED = 501;
    //}

    public class DictConst
    {
        public const String NORMAL_ENABLE = "1";
        public const String NORMAL_DISABLE= "0";
    }

    public class WebConst
    {
        public const String TOKEN_NAME = "x-web-user-token";
    }

    public class MenuConst
    {
        public const String MENU_TYPE_FOLDER = "M";
        public const String MENU_TYPE_MENU = "C";
        public const String MENU_TYPE_BUTTON = "F";
    }
}
