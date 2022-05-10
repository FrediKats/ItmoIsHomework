<template>
  <div id="app">
    <div class="input-container">
      <input type="text" v-model="fileName">
      <button v-on:click="save(fileName,fileContent)">save</button>
      <button v-on:click="load">load</button>
    </div>

    <textarea v-model="fileContent" @input="update"></textarea>
    <div class="output-block" v-html="compiledMarkdown"></div>

    <section>
      <ul>
        <li
          v-for="file in files"
          v-bind:key="file._id"
        ><button v-on:click="renderFile(file)">{{ file.fileName }}</button></li>
      </ul>
    </section>
  </div>
</template>

<script>
import * as marked from "marked";

export default {
  name: "app",

  data: function() {
    return {
      fileContent: "__testing text__",
      fileName: "Testing header",
      files: []
    };
  },

  methods: {
    update: function(element) {
      console.log(element.target.value);
      this.fileContent = element.target.value;
    },
    save: function(fileName, fileContent) {
      const body = {
        fileName,
        fileContent
      };
      fetch("http://localhost:3000/save", {
        method: "POST",
        mode: "cors",
        headers: {
          "Access-Control-Allow-Origin": "*",
          "Content-Type": "application/json"
        },
        body: JSON.stringify(body)
      });
    },
    load: function() {
      fetch("http://localhost:3000/load")
        .then(response => {
          return response.json();
        })
        .then(filesList => {
          this.files = filesList;
        });
    },
    renderFile: function(item) {
      this.fileContent = item.fileContent;
    }
  },

  computed: {
    compiledMarkdown: function() {
      return marked(this.fileContent);
    }
  }
};
</script>

<style>
.input-container {
  width: 100%;
}

html,
body,
#app {
  margin: 0;
  height: 100%;
  font-family: "Helvetica Neue", Arial, sans-serif;
  color: #333;
}

textarea,
#app .output-block {
  display: inline-block;
  width: 49%;
  height: 100%;
  vertical-align: top;
  box-sizing: border-box;
  padding: 0 20px;
}

textarea {
  border: none;
  border-right: 1px solid #ccc;
  resize: none;
  outline: none;
  background-color: #f6f6f6;
  font-size: 14px;
  font-family: "Monaco", courier, monospace;
  padding: 20px;
}

code {
  color: #f66;
}
</style>
