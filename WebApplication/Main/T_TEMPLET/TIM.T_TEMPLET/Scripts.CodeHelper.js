var objHelper;

$(function () {

  if ('onhelp' in document.body) {
    document.body.onhelp = CodeHelper;
  }
  else {
    window.onkeydown = OnKeyDown;
  }
});

function OnKeyDown(event) {
  if (event.keyCode == 112) {
    CodeHelper(event);
    return false;
  }
}

function CodeHelper(e) {
  if (!e) e = window.event;
  objHelper = e.srcElement ? e.srcElement : e.target;
  var ctrlValue = objHelper.value;
  var ctrlId = escape(objHelper.id);
  var codeId;
  var appendParam;

  var i = ctrlId.indexOf('_txt');
  if (i > 0) {
    codeId = ctrlId.substring(i + 4);
    var j = codeId.lastIndexOf('_');
    if (j > 0) {
      codeId = codeId.substring(0, j);
    }
  }
  if ("uppercase" == objHelper.style.textTransform.toLowerCase())
    ctrlValue = ctrlValue.toUpperCase();
  else if ("lowercase" == objHelper.style.textTransform.toLowerCase())
    ctrlValue = ctrlValue.toLowerCase();
  appendParam = $('#SiteTemplet_Templet_CodeHelperParam').val();
  GetCodeHelperUrl(codeId, ctrlId, ctrlValue, appendParam);
  return false;
}

function CallbackSetCtrlValue(value) {
  try {
    if (objHelper) {
      objHelper = document.getElementById(objHelper.id);
      if (value) {
        objHelper.value = value;
        if (objHelper.onchange) {
          objHelper.onchange();
        }
      }
      objHelper.focus();
    }
  }
  catch (e) {
  }
}

function AddPostParam(preParams, paramName, paramValue) {
  if (preParams.length > 0)
    preParams += '&';
  return preParams + encodeURIComponent(paramName) + '=' + encodeURIComponent(paramValue);
}
function AddAppendParam(preParams, paramName, postAppendParams) {
  if (preParams.length > 0)
    preParams += '&';
  return preParams + postAppendParams;
}

function GetCodeHelperUrl(codeId, ctrlId, ctrlValue, postAppendParams) {
  var postParams = '';
  postParams = AddPostParam(postParams, 'CODEID', codeId);
  postParams = AddPostParam(postParams, 'CTRLID', ctrlId);
  postParams = AddPostParam(postParams, 'CTRLVALUE', ctrlValue);
  postParams = AddAppendParam(postParams, 'APPENDVALUE', postAppendParams);

  var wRequest = new Sys.Net.WebRequest();
  wRequest.set_url('../../T_TEMPLET/Handler/CodeHelperHandler.ashx?' + postParams);
  wRequest.set_httpVerb('POST');
  wRequest.set_body();
  wRequest.add_completed(OpenCodeHelper);
  wRequest.invoke();
}

function OpenCodeHelper(executor, eventArgs) {
  if (executor.get_responseAvailable()) {
    var openUrl = executor.get_responseData();
    if (openUrl != '')
      OpenPage('', openUrl, 900, 600, '', CallbackSetCtrlValue);
    else
      alert('无对应代码帮助类！');
  }
  else {
    if (executor.get_timedOut()) {
      alert('连接超时！');
    }
    else if (executor.get_aborted()) {
      alert('操作被终止！');
    }
  }
}
