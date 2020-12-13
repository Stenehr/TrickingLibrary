<template>
    <div>
        <div v-if="tricks">
            <div :key="indx" v-for="(t, indx) in tricks">
                {{ t.id }} - {{ t.name }}
            </div>
        </div>

        <div v-if="submissions">
            <div :key="indx" v-for="(s, indx) in submissions">
                {{ s.description }}
                <div>
                    <video
                        controls
                        :src="`http://localhost:5000/api/videos/${s.video}`"
                    ></video>
                </div>
            </div>
        </div>

        
    </div>
</template>

<script>
import { mapState } from "vuex";

export default {
    computed: {
        ...mapState("tricks", ["tricks"]),
        ...mapState("submissions", ["submissions"]),
    }
};
</script>
