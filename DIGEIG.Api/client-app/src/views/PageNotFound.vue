<template>
  <div class="d-flex justify-content-center align-items-center main">
    <div class="w-25">
      <h2>Pagina no encontrada</h2>
      <b-button
        pill
        variant="primary"
        class="w-50"
        :to="{ name: 'InstitutionStructure' }"
        >Inicio</b-button
      >
    </div>
  </div>
</template>

<script>
export default {
  name: "PageNotFound",
};
</script>

<style scoped>
.main {
  height: 100vh;
}
</style>
