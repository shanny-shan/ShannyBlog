<script setup>
defineProps({
  title: {
    type: String,
  },
})
</script>
<template>
  <div class="flex items-center w-full">
    <font-awesome-icon icon="fa-solid fa-star" class="text-primary" />
    <div class="font-bold ml-2 text-xl">{{ title }}</div>
  </div>
</template>
<style lang="scss" scoped></style>
