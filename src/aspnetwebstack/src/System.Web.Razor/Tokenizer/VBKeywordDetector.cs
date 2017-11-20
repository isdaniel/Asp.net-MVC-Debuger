﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Web.Razor.Tokenizer.Symbols;

namespace System.Web.Razor.Tokenizer
{
    internal static class VBKeywordDetector
    {
        private static readonly Dictionary<string, VBKeyword> _keywords = new Dictionary<string, VBKeyword>(StringComparer.OrdinalIgnoreCase)
        {
            { "addhandler", VBKeyword.AddHandler },
            { "andalso", VBKeyword.AndAlso },
            { "byte", VBKeyword.Byte },
            { "catch", VBKeyword.Catch },
            { "cdate", VBKeyword.CDate },
            { "cint", VBKeyword.CInt },
            { "const", VBKeyword.Const },
            { "csng", VBKeyword.CSng },
            { "culng", VBKeyword.CULng },
            { "declare", VBKeyword.Declare },
            { "directcast", VBKeyword.DirectCast },
            { "else", VBKeyword.Else },
            { "enum", VBKeyword.Enum },
            { "exit", VBKeyword.Exit },
            { "friend", VBKeyword.Friend },
            { "getxmlnamespace", VBKeyword.GetXmlNamespace },
            { "handles", VBKeyword.Handles },
            { "in", VBKeyword.In },
            { "is", VBKeyword.Is },
            { "like", VBKeyword.Like },
            { "mod", VBKeyword.Mod },
            { "mybase", VBKeyword.MyBase },
            { "new", VBKeyword.New },
            { "addressof", VBKeyword.AddressOf },
            { "as", VBKeyword.As },
            { "byval", VBKeyword.ByVal },
            { "cbool", VBKeyword.CBool },
            { "cdbl", VBKeyword.CDbl },
            { "class", VBKeyword.Class },
            { "continue", VBKeyword.Continue },
            { "cstr", VBKeyword.CStr },
            { "cushort", VBKeyword.CUShort },
            { "default", VBKeyword.Default },
            { "do", VBKeyword.Do },
            { "elseif", VBKeyword.ElseIf },
            { "erase", VBKeyword.Erase },
            { "false", VBKeyword.False },
            { "function", VBKeyword.Function },
            { "global", VBKeyword.Global },
            { "if", VBKeyword.If },
            { "inherits", VBKeyword.Inherits },
            { "isnot", VBKeyword.IsNot },
            { "long", VBKeyword.Long },
            { "module", VBKeyword.Module },
            { "myclass", VBKeyword.MyClass },
            { "next", VBKeyword.Next },
            { "alias", VBKeyword.Alias },
            { "boolean", VBKeyword.Boolean },
            { "call", VBKeyword.Call },
            { "cbyte", VBKeyword.CByte },
            { "cdec", VBKeyword.CDec },
            { "clng", VBKeyword.CLng },
            { "csbyte", VBKeyword.CSByte },
            { "ctype", VBKeyword.CType },
            { "date", VBKeyword.Date },
            { "delegate", VBKeyword.Delegate },
            { "double", VBKeyword.Double },
            { "end", VBKeyword.End },
            { "error", VBKeyword.Error },
            { "finally", VBKeyword.Finally },
            { "get", VBKeyword.Get },
            { "gosub", VBKeyword.GoSub },
            { "implements", VBKeyword.Implements },
            { "integer", VBKeyword.Integer },
            { "let", VBKeyword.Let },
            { "loop", VBKeyword.Loop },
            { "mustinherit", VBKeyword.MustInherit },
            { "namespace", VBKeyword.Namespace },
            { "not", VBKeyword.Not },
            { "and", VBKeyword.And },
            { "byref", VBKeyword.ByRef },
            { "case", VBKeyword.Case },
            { "cchar", VBKeyword.CChar },
            { "char", VBKeyword.Char },
            { "cobj", VBKeyword.CObj },
            { "cshort", VBKeyword.CShort },
            { "cuint", VBKeyword.CUInt },
            { "decimal", VBKeyword.Decimal },
            { "dim", VBKeyword.Dim },
            { "each", VBKeyword.Each },
            { "endif", VBKeyword.EndIf },
            { "event", VBKeyword.Event },
            { "for", VBKeyword.For },
            { "gettype", VBKeyword.GetType },
            { "goto", VBKeyword.GoTo },
            { "imports", VBKeyword.Imports },
            { "interface", VBKeyword.Interface },
            { "lib", VBKeyword.Lib },
            { "me", VBKeyword.Me },
            { "mustoverride", VBKeyword.MustOverride },
            { "narrowing", VBKeyword.Narrowing },
            { "nothing", VBKeyword.Nothing },
            { "notinheritable", VBKeyword.NotInheritable },
            { "on", VBKeyword.On },
            { "or", VBKeyword.Or },
            { "overrides", VBKeyword.Overrides },
            { "property", VBKeyword.Property },
            { "rem", VBKeyword.Rem },
            { "readonly", VBKeyword.ReadOnly },
            { "resume", VBKeyword.Resume },
            { "set", VBKeyword.Set },
            { "single", VBKeyword.Single },
            { "string", VBKeyword.String },
            { "then", VBKeyword.Then },
            { "try", VBKeyword.Try },
            { "ulong", VBKeyword.ULong },
            { "wend", VBKeyword.Wend },
            { "with", VBKeyword.With },
            { "notoverridable", VBKeyword.NotOverridable },
            { "operator", VBKeyword.Operator },
            { "orelse", VBKeyword.OrElse },
            { "paramarray", VBKeyword.ParamArray },
            { "protected", VBKeyword.Protected },
            { "redim", VBKeyword.ReDim },
            { "return", VBKeyword.Return },
            { "shadows", VBKeyword.Shadows },
            { "static", VBKeyword.Static },
            { "structure", VBKeyword.Structure },
            { "throw", VBKeyword.Throw },
            { "trycast", VBKeyword.TryCast },
            { "ushort", VBKeyword.UShort },
            { "when", VBKeyword.When },
            { "withevents", VBKeyword.WithEvents },
            { "object", VBKeyword.Object },
            { "option", VBKeyword.Option },
            { "overloads", VBKeyword.Overloads },
            { "partial", VBKeyword.Partial },
            { "public", VBKeyword.Public },
            { "sbyte", VBKeyword.SByte },
            { "shared", VBKeyword.Shared },
            { "step", VBKeyword.Step },
            { "sub", VBKeyword.Sub },
            { "to", VBKeyword.To },
            { "typeof", VBKeyword.TypeOf },
            { "using", VBKeyword.Using },
            { "while", VBKeyword.While },
            { "writeonly", VBKeyword.WriteOnly },
            { "of", VBKeyword.Of },
            { "optional", VBKeyword.Optional },
            { "overridable", VBKeyword.Overridable },
            { "private", VBKeyword.Private },
            { "raiseevent", VBKeyword.RaiseEvent },
            { "removehandler", VBKeyword.RemoveHandler },
            { "select", VBKeyword.Select },
            { "short", VBKeyword.Short },
            { "stop", VBKeyword.Stop },
            { "synclock", VBKeyword.SyncLock },
            { "true", VBKeyword.True },
            { "uinteger", VBKeyword.UInteger },
            { "variant", VBKeyword.Variant },
            { "widening", VBKeyword.Widening },
            { "xor", VBKeyword.Xor }
        };

        public static VBKeyword? GetKeyword(string id)
        {
            VBKeyword type;
            if (!_keywords.TryGetValue(id, out type))
            {
                return null;
            }
            return type;
        }
    }
}