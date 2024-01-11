// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function get_pattern_exp(code, maxLength) {
    var exp = "";
    switch (code) {
        case "name":
            exp = /^[a-zA-Z]+$/;
            break;
        case "date":
            exp = /^$|^((0?[13578]|1[02])[\/.]31[\/.][1-9][0-9]{3})|(([01,1]|0[3-9]|1[1-2])[\/.](29|30)[\/.][1-9][0-9]{3})|((0?[1-9]|1[0-2])[\/.](0?[1-9]|1[0-9]|2[0-8])[\/.][1-9][0-9]{3})|([02,2][\/.]29[\/.](([1-9][0-9](04|08|[2468][048]|[13579][26]))|([2468][0]{3})))$/;
            break;
    }
    return exp;
}
function get_pattern_invalid_text(code) {
    var text = "Invalid";
    switch (code) {
        case "name":
            text = "Invalid - Name must contain only letters";
            break;
        case "date":
            text = "Invalid";
            break;
    }
    return text;
}

function ValidateContainer(_container) {
    var errors = false;
    $('#' + _container + ' input.form-control:not(.no-validation), #' + _container + ' input.textarea:not(.no-validation), #' + _container + ' select.form-select:not(.no-validation)').removeClass('is-invalid');
    $('#' + _container + ' input.form-control:not(.no-validation), #' + _container + ' input.textarea:not(.no-validation)').each(function () {
        var _input = $(this);
        if ($(_input).val().trim() == "" && $(_input).prop('required')) {
            $(_input).next('.invalid-feedback').text('Required');
            $(_input).addClass('is-invalid');
            errors = true;
        } else {
            var maxLength = $(_input).attr("maxlength") || null;
            var _p = $(_input).attr('pattern');
            var value = $(_input).val()
            var regex = new RegExp(get_pattern_exp(_p, maxLength));
            if (!regex.test($(_input).val())) {
                $(_input).next('.invalid-feedback').text(get_pattern_invalid_text(_p));
                $(_input).addClass('is-invalid');
                errors = true;
            }
        }
    });

    $('#' + _container + ' select.form-select:not(.no-validation)').each(function () {
        var _input = $(this);
        if (($(_input).val() == "" || $(_input).val() == null) && $(_input).prop('required')) {
            $(_input).next('.invalid-feedback').text('Required');
            $(_input).addClass('is-invalid');
            errors = true;
        }
    });
    return errors;
}
function EnableLoader(_txt) {
    _txt = _txt || "";
    $(".loader-text").html(_txt);
    $('.loader-layout').show();
}
function DisableLoader() {
    $('.loader-layout').hide();
}

$.fn.extend({
    AddMessages: function (msgs) {
        var _msg = '<div class="alert alert-danger alert-dismissible show">' +
            '<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>' +
            '<div class="alert-icon"><i class="bi bi-x-circle"></i></div>' +
            '<div class="alert-text">' +
            '<h5>Error</h5><ul>';
        for (i = 0; i < msgs.length; i++) {
            var tempmsg = msgs[i].MESSAGE;

            if (tempmsg.indexOf("#") < 0) {
                _msg += '<li>' + tempmsg + '</li>';
                continue;
            }

            var label = "";
            var validate_id = "";
            var errorType = "";

            for (j = 0; j < 3; j++) {
                if (j == 0) {
                    label = tempmsg.slice(0, tempmsg.indexOf("#"));
                } else if (j == 1) {
                    errorType = tempmsg.slice(0, tempmsg.indexOf("#"));
                } else if (j == 2) {
                    validate_id = tempmsg;
                    continue;
                }
                tempmsg = tempmsg.substring(tempmsg.indexOf("#") + 1);
            }
            _msg += '<li>' + label + ((errorType != "") ? (' is ' + errorType) : '') + '</li>';
        }
        _msg += "</ul></div></div>";
        $(this).append(_msg);
    },
    AddSimpleMessage: function (Message, MessageType) {
        MessageType = MessageType || 0;
        var Alert_Class = "danger";
        var additional_text = "<strong>Error!</strong>&nbsp;&nbsp;&nbsp;";
        if (MessageType == 1) {
            Alert_Class = "success";
            additional_text = "";
        } else if (MessageType == 2) {
            Alert_Class = "success alert-bold";
            additional_text = "";
        } else if (MessageType == 3) {
            Alert_Class = "danger alert-bold";
            additional_text = "";
        } else if (MessageType == 4) {
            Alert_Class = "info alert-bold";
            additional_text = "";
        } else if (MessageType == 5) {
            Alert_Class = "warning alert-bold";
            additional_text = "";
        } else if (MessageType == 6) {
            Alert_Class = "warning alert-bold alert-margin-top";
            additional_text = "";
        }
        var msg = '<div class="alert alert-' + Alert_Class + ' alert-dismissible show" role="alert"><button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>' + additional_text + Message + '</div>';
        $(this).append(msg);
    },
    AddMessage: function (errorType, label, validateid) {
        var msg = '<div class="alert alert-danger alert-dismissible show" role="alert" data-id="' + validateid + '"><button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button><strong>Error!</strong>&nbsp;&nbsp;&nbsp;<span class="alert-clickable" onclick="GoToValidateID(\'' + validateid + '\');">' + label + '</span> is ' + errorType + '</div>';
        $(this).append(msg);
    }
});
//Element.classList
"document" in self && ("classList" in document.createElement("_") ? !function () { "use strict"; var t = document.createElement("_"); if (t.classList.add("c1", "c2"), !t.classList.contains("c2")) { var e = function (t) { var e = DOMTokenList.prototype[t]; DOMTokenList.prototype[t] = function (t) { var n, i = arguments.length; for (n = 0; i > n; n++) t = arguments[n], e.call(this, t) } }; e("add"), e("remove") } if (t.classList.toggle("c3", !1), t.classList.contains("c3")) { var n = DOMTokenList.prototype.toggle; DOMTokenList.prototype.toggle = function (t, e) { return 1 in arguments && !this.contains(t) == !e ? e : n.call(this, t) } } t = null }() : !function (t) { "use strict"; if ("Element" in t) { var e = "classList", n = "prototype", i = t.Element[n], s = Object, r = String[n].trim() || function () { return this.replace(/^\s+|\s+$/g, "") }, o = Array[n].indexOf || function (t) { for (var e = 0, n = this.length; n > e; e++) if (e in this && this[e] === t) return e; return -1 }, a = function (t, e) { this.name = t, this.code = DOMException[t], this.message = e }, c = function (t, e) { if ("" === e) throw new a("SYNTAX_ERR", "An invalid or illegal string was specified"); if (/\s/.test(e)) throw new a("INVALID_CHARACTER_ERR", "String contains an invalid character"); return o.call(t, e) }, l = function (t) { for (var e = r.call(t.getAttribute("class") || ""), n = e ? e.split(/\s+/) : [], i = 0, s = n.length; s > i; i++) this.push(n[i]); this._updateClassName = function () { t.setAttribute("class", this.toString()) } }, u = l[n] = [], f = function () { return new l(this) }; if (a[n] = Error[n], u.item = function (t) { return this[t] || null }, u.contains = function (t) { return t += "", -1 !== c(this, t) }, u.add = function () { var t, e = arguments, n = 0, i = e.length, s = !1; do t = e[n] + "", -1 === c(this, t) && (this.push(t), s = !0); while (++n < i); s && this._updateClassName() }, u.remove = function () { var t, e, n = arguments, i = 0, s = n.length, r = !1; do for (t = n[i] + "", e = c(this, t); -1 !== e;) this.splice(e, 1), r = !0, e = c(this, t); while (++i < s); r && this._updateClassName() }, u.toggle = function (t, e) { t += ""; var n = this.contains(t), i = n ? e !== !0 && "remove" : e !== !1 && "add"; return i && this[i](t), e === !0 || e === !1 ? e : !n }, u.toString = function () { return this.join(" ") }, s.defineProperty) { var h = { get: f, enumerable: !0, configurable: !0 }; try { s.defineProperty(i, e, h) } catch (g) { -2146823252 === g.number && (h.enumerable = !1, s.defineProperty(i, e, h)) } } else s[n].__defineGetter__ && i.__defineGetter__(e, f) } }(self));
//Element.remove
"remove" in Element.prototype || (Element.prototype.remove = function () { this.parentElement.removeChild(this) });
//Array.prototype.map
Array.prototype.map || (Array.prototype.map = function (r, t) { var n, o, e; if (null == this) throw new TypeError("This is null or not defined"); var i = Object(this), a = i.length >>> 0; if ("function" != typeof r) throw new TypeError(r + " is not a function"); for (arguments.length > 1 && (n = t), o = new Array(a), e = 0; a > e;) { var p, f; e in i && (p = i[e], f = r.call(n, p, e, i), o[e] = f), e++ } return o });
//Array.prototype.slice
!function () { if (navigator.appVersion.indexOf("MSIE 8") > 0) { var r = Array.prototype.slice; Array.prototype.slice = function () { if (this instanceof Array) return r.apply(this, arguments); for (var t = [], n = arguments.length >= 1 ? arguments[0] : 0, e = arguments.length >= 2 ? arguments[1] : this.length, a = n; e > a; a++) t.push(this[a]); return t } } }();
//Array.prototype.indexOf
Array.prototype.indexOf || (Array.prototype.indexOf = function (r, t) { var n; if (null == this) throw new TypeError('"this" is null or not defined'); var e = Object(this), i = e.length >>> 0; if (0 === i) return -1; var a = +t || 0; if (1 / 0 === Math.abs(a) && (a = 0), a >= i) return -1; for (n = Math.max(a >= 0 ? a : i - Math.abs(a), 0); i > n;) { if (n in e && e[n] === r) return n; n++ } return -1 });
//Element.nextElementSibling (previous too)
!function (e) { document.createElement("div").firstElementChild === e && (Object.defineProperty(Element.prototype, "firstElementChild", { get: function () { var e = this.firstChild; do { if (1 === e.nodeType) return e; e = e.nextSibling } while (e); return null } }), Object.defineProperty(Element.prototype, "lastElementChild", { get: function () { var e = this.lastChild; do { if (1 === e.nodeType) return e; e = e.previousSibling } while (e); return null } }), Object.defineProperty(Element.prototype, "nextElementSibling", { get: function () { for (var e = this.nextSibling; e;) { if (1 === e.nodeType) return e; e = e.nextSibling } return null } }), Object.defineProperty(Element.prototype, "previousElementSibling", { get: function () { for (var e = this.previousSibling; e;) { if (1 === e.nodeType) return e; e = e.previousSibling } return null } })) }();
if (!Array.prototype.findIndex) {
    Object.defineProperty(Array.prototype, 'findIndex', {
        value: function (predicate) {
            // 1. Let O be ? ToObject(this value).
            if (this == null) {
                throw new TypeError('"this" is null or not defined');
            }

            var o = Object(this);

            // 2. Let len be ? ToLength(? Get(O, "length")).
            var len = o.length >>> 0;

            // 3. If IsCallable(predicate) is false, throw a TypeError exception.
            if (typeof predicate !== 'function') {
                throw new TypeError('predicate must be a function');
            }

            // 4. If thisArg was supplied, let T be thisArg; else let T be undefined.
            var thisArg = arguments[1];

            // 5. Let k be 0.
            var k = 0;

            // 6. Repeat, while k < len
            while (k < len) {
                // a. Let Pk be ! ToString(k).
                // b. Let kValue be ? Get(O, Pk).
                // c. Let testResult be ToBoolean(? Call(predicate, T, « kValue, k, O »)).
                // d. If testResult is true, return k.
                var kValue = o[k];
                if (predicate.call(thisArg, kValue, k, o)) {
                    return k;
                }
                // e. Increase k by 1.
                k++;
            }

            // 7. Return -1.
            return -1;
        },
        configurable: true,
        writable: true
    });
}
String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.replace(new RegExp(search, 'g'), replacement);
};

Number.prototype.moneyformat = function (n, x) {
    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\.' : '$') + ')';
    return this.toFixed(Math.max(0, ~~n)).replace(new RegExp(re, 'g'), '$&,');
};

if (typeof String.prototype.trim !== 'function') {
    String.prototype.trim = function () {
        return this.replace(/^\s+|\s+$/g, '');
    };
}

jQuery.extend(jQuery.fn.dataTableExt.oSort, {
    "date-range-pre": function (a) {
        var monthArr = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
        return monthArr.indexOf(a);
    },
    "date-range-asc": function (a, b) {
        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },
    "date-range-desc": function (a, b) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    }
});