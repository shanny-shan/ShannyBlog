<script setup>
import AboutDialog from '@/views/_components/dialog/AboutDialog.vue'
import { onMounted, ref } from 'vue'
import { useAdminStore } from '@/stores/modules/admin'
import { useAboutStore } from '@/stores/modules/about'
const adminStore = useAdminStore()
const aboutStore = useAboutStore()
const aboutList = ref([])

const getAboutList = async () => {
  const res = await aboutStore.getAboutMe()
  console.log(res.data.data)
  if (res.data.code.toLowerCase() === 'success') {
    aboutList.value = res.data.data || []
  }
}
onMounted(() => {
  getAboutList()
})
</script>
<template>
  <div>
    <div class="flex justify-end gap-2">
      <button class="btn btn-primary" @click="adminStore.openDialog('about')">
        Add about
      </button>
    </div>
    <div class="overflow-x-auto mt-2">
      <table class="table">
        <thead>
          <tr>
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <th>Name</th>
            <th>Tag</th>
            <th>Introduce</th>
            <th>GitHub</th>
            <th>Steam</th>
            <th>Web</th>
            <th>Bilibili</th>
            <th>IsActive</th>
            <th>CreateTime</th>
            <th>UpdateTime</th>
            <th>Edit</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in aboutList">
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <td>{{ item.name }}</td>
            <td>{{ item.tag }}</td>
            <td>{{ item.introduce }}</td>
            <td>{{ item.github }}</td>
            <td>{{ item.steam }}</td>
            <td>{{ item.web }}</td>
            <td>{{ item.biliBili }}</td>
            <td>{{ item.isActive }}</td>
            <td>{{ item.createTime }}</td>
            <td>{{ item.updateTime }}</td>
            <th>
              <div class="flex gap-2">
                <button class="btn btn-ghost btn-xs">Edit</button>
                <button class="btn btn-ghost btn-xs">Delete</button>
              </div>
            </th>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
  <AboutDialog />
</template>
<style lang="scss" scoped></style>
