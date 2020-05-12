using ArnoAdminCore.Base.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ArnoAdminCore.Base.Models
{
    public class Result
    {
        public int code { get; set; }
        public String msg { get; set; }
        public object data { get; set; }

        public  Result()
        {

        }
        public Result(int code, String msg)
        {
            this.code = code;
            this.msg = msg;
        }
        public Result(int code, String msg, object data)
        {
            this.code = code;
            this.msg = msg;
            this.data = data;
        }
        public static Result Ok(String msg, Object data)
        {
            return new Result(HttpStatus.SUCCESS, msg, data);
        }
        public static Result Ok(Object data)
        {
            return Result.Ok("操作成功", data);
        }
        public static Result Ok(String msg)
        {
            return Result.Ok(msg, null);
        }
        public static Result Ok()
        {
            return Result.Ok("操作成功");
        }
        public static Result Error()
        {
            return Result.Error("操作失败");
        }
        public static Result Error(String msg)
        {
            return Result.Error(msg, null);
        }
        public static Result Error(String msg, Object data)
        {
            return new Result(HttpStatus.ERROR, msg, data);
        }
    }
}
