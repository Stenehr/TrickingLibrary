import { UPLOAD_TYPE } from "../data/enums";

const initState = () => ({
    uploadPromise: null,
    active: false,
    type: "",
    step: 1,
})

export const state = initState;

export const mutations = {
    toggleActivity(state) {
        state.active = !state.active;
        if (!state.active) {
            Object.assign(state, initState());
        }
    },

    setType(state, { type }) {
        state.type = type;
        if (type === UPLOAD_TYPE.TRICK) {
            state.step++;
        } else if (type === UPLOAD_TYPE.SUBMISSION) {
            state.step += 2;
        }
    },

    setUploadPromise(state, { uploadPromise }) {
        state.uploadPromise = uploadPromise;
        state.step++;
    },

    incrementStep(state) {
        state.step++;
    },

    reset(state) {
        Object.assign(state, initState())
    }
}

export const actions = {
    startVideoUpload({ commit }, { form }) {
        const uploadPromise = this.$axios.$post("/api/videos", form);
        commit("setUploadPromise", { uploadPromise })
    },
    
    async createTrick({ dispatch }, { trick, submission }) {
        const createdTrick = await this.$axios.$post("/api/tricks", trick);
        submission.trickId = createdTrick.id;
        const createdSubmission = await this.$axios.$post("/api/submissions", submission);

        await dispatch("tricks/fetchTricks", null, { root: true });
        await dispatch("submissions/fetchSubmissions", null, { root: true });
    }
}
