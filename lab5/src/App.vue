<template>
  <div id="app">
    <div class="input-container">
      <input type="text" :value="title">
      <button v-on:click="save(title,input)">save</button>
    </div>

    <textarea :value="input" @input="update"></textarea>
    <div class="output-block" v-html="compiledMarkdown"></div>
  </div>
</template>

<script>
import * as marked from "marked";

export default {
  name: "app",

  data: function() {
    return {
      input: "__testing text__",
      title: "Testing header"
    };
  },

  methods: {
    update: function(element) {
      this.input = element.target.value;
    },
    save: function(title, content) {
      const body = {
        title,
        content
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
    }
  },

  computed: {
    compiledMarkdown: function() {
      return marked(this.input);
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
