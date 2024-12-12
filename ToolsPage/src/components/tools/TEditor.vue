<template>
  <div class="app-container child_boxSide">
    <editor id="tinymce" v-model="tinymceHtml" :init="init"></editor>
  </div>
</template>
<script setup>
import { ref, onMounted, watchEffect } from "vue";
import store from "@/utils/store"
import tinymce from "tinymce/tinymce";
import "tinymce/models/dom"; // 特别注意 tinymce 6.0.0 版本之后必须引入，否则不显示
import "tinymce/themes/silver/theme";
import Editor from "@tinymce/tinymce-vue"; // 引入组件
// 都是富文本插件
import "tinymce/icons/default";
import "tinymce/plugins/image";
import "tinymce/plugins/media";
import "tinymce/plugins/link";
import "tinymce/plugins/code";
import "tinymce/plugins/codesample"
import "tinymce/plugins/table";
import "tinymce/plugins/lists";
import "tinymce/plugins/wordcount";
// 以上所有的样式在 node_modules 下面 tinymce 里面的 plugins 都能找到。
const tinymceHtml = ref("请输入内容");

// 将编辑器的内容都记录下来
store.state.editor.tinyHtml = tinymceHtml;

const init = {
  //初始化数据
  language_url: "/tinymce/langs/zh-Hans.js", // 引入语言包（该语言包在public下，注意文件名称）
  language: "zh-Hans", // 这里名称根据 zh-Hans.js 里面写的名称而定
  skin_url: "/tinymce/skins/ui/CUSTOM", // 这里引入的样式
  height: 450, // 限制高度
  plugins: "link lists image media code table wordcount codesample", // 富文本插件
  toolbar:
    "bold italic underline strikethrough | fontsizeselect | forecolor backcolor | alignleft aligncenter alignright alignjustify | codesample | bullist numlist | outdent indent blockquote | undo redo | link unlink image code media | removeformat",
  branding: false, // //是否禁用“Powered by TinyMCE”
  paste_data_images: true,
  menubar: true, //顶部菜单栏显示
  codesample_languages: [{
    text: 'HTML/XML',
    value: 'html'
  }, {
    text: 'JavaScript',
    value: 'javascript'
  }, {
    text: 'CSS',
    value: 'css'
  }, {
    text: 'PHP',
    value: 'php'
  },
  {
    text: 'Ruby',
    value: 'ruby'
  },
  {
    text: 'Python',
    value: 'python'
  },
  {
    text: 'Java',
    value: 'java'
  },
  {
    text: 'C',
    value: 'c'
  },
  {
    text: 'C#',
    value: 'csharp'
  },
  {
    text: 'C++',
    value: 'cpp'
  }], // 代码块可供选择的代码语言
  // paste_convert_word_fake_lists: false, // 插入word文档需要该属性
  content_css: "/tinymce/skins/content/CUSTOM/content.css", //以css文件方式自定义可编辑区域的css样式，css文件需自己创建并引入
  images_upload_handler: (blobInfo, success, failure) => {
    // console.log(blobInfo.blob());
    // 上传图片需要，FormData 格式的文件；
    const formDataUp = new FormData();
    formDataUp.append("img", blobInfo.blob());
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve(URL.createObjectURL(blobInfo.blob()));
      }, 500);
    });
  } // 图片上传功能
};
onMounted(() => {
  tinymce.init({}); // 初始化富文本
});
</script>