<script setup>
import { onMounted } from 'vue'
import TitleComponent from '@/components/home/TitleComponent.vue'
import { useAboutStore } from '@/stores/modules/about'
const aboutStore = useAboutStore()
onMounted(async () => {
  const res = await aboutStore.getAboutMe()
  if (res.data.code.toLowerCase() === 'success') {
    aboutStore.authorInfo = res.data.data
  }
})
</script>
<template>
  <div class="w-full">
    <TitleComponent title="About Me" />
    <div class="card bg-base-200 shadow-sm mt-3 md:mt-5 w-full">
      <div class="card-body">
        <div class="card-title flex items-center">
          <div class="avatar">
            <div class="w-18 rounded-full shadow-xl">
              <img src="@/assets/images/avatar.jpg" />
            </div>
          </div>
          <div class="ml-2">
            <h1>{{ aboutStore.authorInfo.name }}</h1>
            <div class="font-light text-sm text-neutral">
              {{ aboutStore.authorInfo.tag }}
            </div>
          </div>
        </div>
        <p class="mt-4">
          {{ aboutStore.authorInfo.introduce }}
        </p>
        <div class="card-actions">
          <div class="mt-4">
            <a
              class="tooltip"
              data-tip="GitHub"
              :href="aboutStore.authorInfo.github"
              target="_blank"
            >
              <font-awesome-icon
                icon="fa-brands fa-github"
                class="text-xl hover:text-primary"
              />
            </a>
            <!-- <a
              class="ml-4"
              :href="aboutStore.authorInfo.biliBili"
              target="_blank"
            >
              <font-awesome-icon
                icon="fa-brands fa-bilibili"
                class="text-xl icon-bilibili"
              />
            </a> -->
            <!-- <a class="ml-4" href="/" target="_blank">
              <font-awesome-icon
                icon="fa-brands fa-steam"
                class="text-xl icon-steam hover:text-primary"
              />
            </a> -->
            <a
              class="ml-4 tooltip"
              data-tip="WebSite"
              href="https://www.shanny.wang"
              target="_blank"
            >
              <font-awesome-icon
                icon="fa-solid fa-earth-asia"
                class="text-xl icon-website"
              />
            </a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<style lang="scss" scoped></style>
