const initState = () => ({
    submissions: []
});

export const state = initState;

export const mutations = {
    setSubmissions(state, { submissions }) {
        state.submissions = submissions;
    },

    reset(state) {
        Object.assign(state, initState());
    }
}

export const actions = {
    async fetchSubmissionsForTrick({ commit }, { trickId }) {
        const submissions = await this.$axios.$get(`/api/tricks/${trickId}/submissions`);
        commit("setSubmissions", { submissions });
    },

    createSubmission(context, { form }) {
        return this.$axios.$post("/api/submissions", form);
    }
}