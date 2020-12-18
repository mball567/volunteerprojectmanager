<template>
  <div id="app">
    <header>
      <h1 v-on:click="$router.push({path: '/profiles/my'})">akt</h1>
    </header>
    
    <router-view />

  </div>
</template> 
<script>
export default {
  
}
</script>
<style>
@import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;700&display=swap');

body {
  background-color: #023047ff;
  font-family: "Montserrat", sans-serif;
  font-weight: 400;
}

div#app header h1{
  color:#ffb703ff;
  font-size: 5em;
  font-weight: 700;
  margin-block: 0.25em;
  padding-left: 0.25em;
  text-decoration: none;
}

div#app header h1:hover{
  color: white;
  cursor: pointer;
}


</style>