<template>
  <div id="app">
    <header>
        <h1>akt</h1>
    </header>
    <div id="nav">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
    </div>
    <router-view />
  </div>
</template> 
<script>
export default {
  
}
</script>
<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;700&display=swap');

div#app header h1 {
  color:#ffb703ff;
  font-size: 5em;
  font-weight: 700;
  margin-block: 0.25em;
  padding-left: 0.25em;
}


</style>