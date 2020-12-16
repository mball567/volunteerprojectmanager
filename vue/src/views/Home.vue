<template>
  <div class="home">
    <h1>Home</h1>
    <p>You must be authenticated to see this</p>
  </div>
</template>

<script>
import WebService from '../services/WebService.js'

export default {
  name: "home",
  created(){
    WebService.getCauses()
      .then( (response) => {
      this.$store.commit("SET_CAUSES", response.data);
      });  
  }
};
</script>

<style> 
h1 {
  color: white;
}

p {
  color: white;
}

</style>