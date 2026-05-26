<script setup>
import { ref, onMounted } from 'vue'
import { useProjectInfoStore } from '@/stores/modules/project'
const year = new Date().getFullYear()
const month = new Date().getMonth() + 1
const projectInfoStore = useProjectInfoStore()
const projectInfo = ref({})
const getProjectInfo = async () => {
  const res = await projectInfoStore.getProjectInfos()
  if (res.data.code.toLowerCase() === 'success') {
    projectInfo.value = res.data.data
  }
}
onMounted(() => {
  getProjectInfo()
})
</script>
<template>
  <footer
    class="footer footer-horizontal footer-center bg-base-200 text-base-content rounded p-5 md:p-15 mt-5 md:mt-10"
  >
    <div class="md:w-7/10">
      <!-- <nav>
        <div class="grid grid-flow-col gap-4">
          <a
            href="https://github.com/shanny-shan?tab=repositories"
            target="_blank"
          >
            <font-awesome-icon
              icon="fa-brands fa-github"
              class="text-2xl md:text-3xl hover:text-primary"
            />
          </a>
          <a
            class="ml-4"
            href="https://space.bilibili.com/335353354?spm_id_from=333.1007.0.0"
            target="_blank"
          >
            <font-awesome-icon
              icon="fa-brands fa-bilibili"
              class="text-2xl md:text-3xl icon-bilibili"
            />
          </a>
          <a class="ml-4" href="/" target="_blank">
            <font-awesome-icon
              icon="fa-brands fa-steam"
              class="text-2xl md:text-3xl icon-steam hover:text-primary"
            />
          </a>
        </div>
      </nav> -->
      <aside class="mt-3 md:mt-5">
        <p class="text-xs md:text-base">
          © 2025.9–{{ year }}.{{ month }}
          <!-- {{ projectInfo.owner || 'Shanny' }} · -->
          {{ projectInfo.name || 'ShannyBlog' }}
          <!-- · v{{
            projectInfo.version || '0.0.0'
          }} -->
        </p>
      </aside>
    </div>
  </footer>
</template>

<style lang="scss" scoped></style>
