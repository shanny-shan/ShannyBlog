<script setup>
import TitleComponent from '@/components/home/TitleComponent.vue'
</script>
<template>
  <div class="w-full">
    <TitleComponent title="Article Information" />
    <div class="card bg-base-200 shadow-sm mt-3 md:mt-5 w-full">
      <div class="card-body w-full">
        <div class="flex justify-start items-center w-full">
          <font-awesome-icon icon="fa-solid fa-tag" class="text-primary" />
          <div class="ml-2 flex items-center">
            <span class="font-bold text-natural">Category:</span>
            <div class="hover:underline ml-2">
              <a href="/" class="text-natural">Vue</a>
            </div>
          </div>
        </div>
        <div class="flex justify-start items-center w-full mt-2">
          <font-awesome-icon icon="fa-solid fa-clock" class="text-primary" />
          <div class="ml-2 flex items-center">
            <span class="font-bold text-natural">Updated:</span>
            <div class="hover:underline ml-2">
              <a href="/" class="text-natural">March 27,2024</a>
            </div>
          </div>
        </div>
        <div class="flex justify-start items-center w-full mt-2">
          <font-awesome-icon icon="fa-solid fa-user" class="text-primary" />
          <div class="ml-2 flex items-center">
            <span class="font-bold text-natural">Author:</span>
            <div class="hover:underline ml-2">
              <a href="/" class="text-natural">Shanny</a>
            </div>
          </div>
        </div>
        <div class="flex justify-start items-center w-full mt-2">
          <font-awesome-icon icon="fa-solid fa-bell" class="text-primary" />
          <div class="ml-2 flex items-center">
            <span class="font-bold text-natural">Read:</span>
            <div class="hover:underline ml-2">
              <a href="/" class="text-natural">2</a>
            </div>
          </div>
        </div>
        <div class="flex justify-start items-center w-full mt-2">
          <font-awesome-icon icon="fa-solid fa-award" class="text-primary" />
          <div class="ml-2 flex items-center">
            <span class="font-bold text-natural">Rating:</span>
            <div class="ml-2 rating rating-xs">
              <input
                v-for="i in 5"
                disabled
                type="radio"
                :name="'rating-' + i"
                class="mask mask-star-2 bg-warning"
                aria-label="1 star"
                :checked="i == 3"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<style lang="scss" scoped></style>
