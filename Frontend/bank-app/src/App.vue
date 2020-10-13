<template>
  <div id="app">
    <BankApp/>
  </div>
</template>

<script>
import BankApp from './components/BankApp.vue'

export default {
  name: 'App',
  components: {
    BankApp
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
