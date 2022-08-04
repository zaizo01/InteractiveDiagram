<template>
  <div id="app">
    <TheHeading />
    <router-view />
    <TheFooting />
  </div>
</template>

<script>
import TheHeading from "./components/TheHeading.vue";
import TheFooting from "./components/TheFooting.vue";

export default {
  name: "App",
  components: {
    TheHeading,
    TheFooting,
  },
};
</script>

<style lang="scss">
@import "./assets/styles/custom.scss";
@import "~bootstrap/dist/css/bootstrap.css";
@import "~bootstrap-vue/dist/bootstrap-vue.css";

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
